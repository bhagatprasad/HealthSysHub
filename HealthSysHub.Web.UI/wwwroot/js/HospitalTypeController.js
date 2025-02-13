function HospitalTypeController() {
    var self = this;

    self.selectedRows = [];

    self.currectSelectedHospitalType = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedHospitalTypes = [];

    self.init = function () {
        var table = new Tabulator("#HospitalTypeGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/HospitalType/FetchHospitalTypes',
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentHospitalTypeChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().HospitalTypeId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childHospitalTypeChkbox-${rowId}' class='childHospitalTypeChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "HospitalType Name", field: "HospitalTypeName" },
                { title: "Description", field: "Description" },
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
                $('#parentHospitalTypeChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().HospitalTypeId === changedRow.HospitalTypeId);

                    if (foundRow) {
                        var rowId = foundRow.getData().HospitalTypeId;
                        var checkbox = document.querySelector(`#childHospitalTypeChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedHospitalType = changedRow;
                        }
                        else {
                            self.currectSelectedHospitalType = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentHospitalTypeChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childHospitalTypeChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childHospitalTypeChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.HospitalTypeId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentHospitalTypeChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedHospitalType) {
                $("#Name").val(self.currectSelectedHospitalType.Name);
                $("#Code").val(self.currectSelectedHospitalType.Code);
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
            $('#AddEditHospitalTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditHospitalTypeForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditHospitalTypeForm');
            var hospitalType = addCommonProperties(formData);
            hospitalType.HospitalTypeId = self.currectSelectedHospitalType ? self.currectSelectedHospitalType.HospitalTypeId : null;

            self.addeditHospitalType(hospitalType, false);
        });

        makeFormGeneric('#AddEditHospitalTypeForm', '#btnsubmit');
        self.addeditHospitalType = function (hospitalType, iscopy) {
            makeAjaxRequest({
                url: "/HospitalType/InsertOrUpdateHospitalType",
                data: hospitalType,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditHospitalTypeForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedHospitalType = {};
                    }
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });
        };
       
    };
}