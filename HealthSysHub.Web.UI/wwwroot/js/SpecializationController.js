function SpecializationController() {
    var self = this; 

    self.selectedRows = [];

    self.currectSelectedSpecialization = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedSpecializations = [];

    self.init = function () {
        var table = new Tabulator("#SpecializationGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/Specialization/FetchSpecializations',
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentSpecializationChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().SpecializationId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childSpecializationChkbox-${rowId}' class='childSpecializationChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Specialization", field: "SpecializationName" },
                { title: "Specialization Description", field: "SpecializationDescription" },
                { title: "Created By", field: "CreatedBy" },
                { title: "Created On", field: "CreatedOn" },
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
                $('#parentSpecializationChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().SpecializationId === changedRow.SpecializationId);

                    if (foundRow) {
                        var rowId = foundRow.getData().SpecializationId;
                        var checkbox = document.querySelector(`#childSpecializationChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedSpecialization = changedRow;
                        }
                        else {
                            self.currectSelectedSpecialization = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentSpecializationChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childSpecializationChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childSpecializationChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.SpecializationId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentSpecializationChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedSpecialization) {
                $("#SpecializationName").val(self.currectSelectedSpecialization.SpecializationName);
                $("#SpecializationDescription").val(self.currectSelectedSpecialization.SpecializationDescription);
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
            $('#AddEditSpecializationForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditSpecializationForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditSpecializationForm');
            var specialization = addCommonProperties(formData);
            specialization.SpecializationId = self.currectSelectedSpecialization ? self.currectSelectedSpecialization.SpecializationId : null;

            self.addeditSpecialization(specialization, false);
        });

        makeFormGeneric('#AddEditSpecializationForm', '#btnsubmit');
        self.addeditSpecialization = function (specialization, iscopy) {
            makeAjaxRequest({
                url: "/Specialization/InsertOrUpdateSpecialization",
                data: specialization,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditSpecializationForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedSpecialization = {};
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
                self.ImportedSpecializations = importedData;
                console.log(self.ImportedSpecializations);
            });
        });

        $(document).on("click", "#uploadButton", function (e) {
            if (self.ImportedTenants.length > 0) {
                makeAjaxRequest({
                    url: API_URLS.BulkInsertOrUpdateTenant,
                    data: self.ImportedTenants,
                    type: 'POST',
                    successCallback: function (response) {
                        self.ImportedSpecializations = [];
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