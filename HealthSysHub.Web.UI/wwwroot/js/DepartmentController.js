function DepartmentController() {
    var self = this;

    self.selectedRows = [];

    self.currectSelectedDepartment = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedDepartments = [];

    self.init = function () {
        var table = new Tabulator("#DepartmentGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/Department/FetchDepartments',
            ajaxParams: {},
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            },
            ajaxResponse: function (url, params, response) {
                return response.data;
            },
            columns: [
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentDepartmentChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().DepartmentId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childDepartmentChkbox-${rowId}' class='childDepartmentChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Department Name", field: "DepartmentName" },
                { title: "Department Description", field: "DepartmentDescription" },
                { title: "Created By", field: "CreatedBy"},
                { title: "Created On", field: "CreatedOn"},
                { title: "Modified By", field: "ModifiedBy" },
                { title: "Modified On", field: "ModifiedOn" },
                {
                    title: "Is Active",
                    field: "IsActive",
                    formatter: "tickCross",
                    align: "center"
                }

            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentDepartmentChkbox').prop('checked', allSelected);
                disableAllButtons();

                // Enable buttons based on selection
                if (rows.length > 0) {
                    enableButtons(table);
                }

                // Find the most recently changed row
                let currentSelectedRows = rows.map(row => row.getData());
                let changedRow = null;

                if (self.selectedRows.length > currentSelectedRows.length) {
                    // A row was deselected
                    changedRow = self.selectedRows.find(row => !currentSelectedRows.includes(row));
                } else if (self.selectedRows.length < currentSelectedRows.length) {
                    // A row was selected
                    changedRow = currentSelectedRows.find(row => !self.selectedRows.includes(row));
                }


                // Update the previous selected rows state
                self.selectedRows = currentSelectedRows;
                // Handle the changed row data
                if (changedRow) {
                    var rows = table.getRows();
                    var foundRow = rows.find(row => row.getData().DepartmentId === changedRow.DepartmentId);

                    if (foundRow) {
                        var rowId = foundRow.getData().DepartmentId;
                        var checkbox = document.querySelector(`#childDepartmentChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedDepartment = changedRow;
                        }
                        else {
                            self.currectSelectedDepartment = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentDepartmentChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childDepartmentChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childDepartmentChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.DepartmentId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentDepartmentChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedDepartment) {
                $("#Name").val(self.currectSelectedDepartment.Name);
                $("#Code").val(self.currectSelectedDepartment.Code);
                $('#sidebar').addClass('show');
                $('body').append('<div class="modal-backdrop fade show"></div>');
            } else {
                $('#sidebar').removeClass('show');
                $('.modal-backdrop').remove();
            }

        });

        $('#addBtn').on('click', function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $('#closeSidebar, .modal-backdrop').on('click', function () {
            $('#AddEditDepartmentForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditDepartmentForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditDepartmentForm');
            var department = addCommonProperties(formData);
            department.DepartmentId = self.currectSelectedDepartment ? self.currectSelectedDepartment.DepartmentId : null;

            self.addeditDepartment(department, false);
        });

        makeFormGeneric('#AddEditDepartmentForm', '#btnsubmit');
        self.addeditDepartment = function (department, iscopy) {
            makeAjaxRequest({
                url: API_URLS.InsertOrUpdateRoleAsync,
                data: department,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditDepartmentForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedDepartment = {};
                    }
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });
        };
        //---------------Import Functionality-------------//
        $(document).on("click", "#importBtn", function () {
            self.selectedRows = [];
            $(self.fileUploadModal).modal('show');
        });
        $(document).on('change', '#fileInput', function (e) {
            var files = e.target.files;
            processFiles(files, gridColumns.RoleGrid, function (importedData) {
                self.ImportedDepartments = importedData;
                console.log(self.ImportedDepartments);
            });
        });

        $(document).on("click", "#uploadButton", function (e) {
            if (self.ImportedTenants.length > 0) {
                makeAjaxRequest({
                    url: API_URLS.BulkInsertOrUpdateTenant,
                    data: self.ImportedTenants,
                    type: 'POST',
                    successCallback: function (response) {
                        self.ImportedDepartments = [];
                        table.setData();
                        $(self.fileUploadModal).modal('hide');
                        console.info(response);
                    },
                    errorCallback: function (xhr, status, error) {
                        console.error("Error in upserting data to server: " + error);
                    }
                });
            }
        });

        //-------------------Export ------------------//
        $(document).on("click", "#exportTemplate", function (e) {
            var _selectedRows = [];

            self.exportExcel(_selectedRows)
        });
        $(document).on("click", "#exportWithGridData", function (e) {
            if (self.selectedRows.length > 0)
                self.exportExcel(self.selectedRows);
        });
        $(document).on("click", "#exportWithOriginalData", function (e) {
            var gridData = table.getData();
            self.exportExcel(gridData);
        });
        self.exportExcel = function (data) {
            var sorters = table.getSorters();
            var sortColumns = sorters.length > 0 ? sorters[0].field : null;
            var sortOrder = sorters.length > 0 ? sorters[0].dir : null;
            exportToExcel(data, gridColumns.RoleGrid, "Role", "Role_Report", sortColumns, sortOrder);
        }
    };
}