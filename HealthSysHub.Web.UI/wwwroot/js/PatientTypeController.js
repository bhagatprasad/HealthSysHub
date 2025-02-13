function PatientTypeController() {

    var self = this;

    self.selectedRows = [];

    self.currectSelectedPatientType = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedPatientTypes = [];

    self.init = function () {
        var table = new Tabulator("#PatientTypeGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/PatientType/FetchPatientTypes',
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentPatientTypeChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().PatientTypeId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childPatientTypeChkbox-${rowId}' class='childPatientTypeChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "PatientType Name", field: "PatientTypeName" },
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
                $('#parentPatientTypeChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().PatientTypeId === changedRow.PatientTypeId);

                    if (foundRow) {
                        var rowId = foundRow.getData().PatientTypeId;
                        var checkbox = document.querySelector(`#childPatientTypeChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedPatientType = changedRow;
                        }
                        else {
                            self.currectSelectedPatientType = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentPatientTypeChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childPatientTypeChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childPatientTypeChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.PatientTypeId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentPatientTypeChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedPatientType) {
                $("#PatientTypeName").val(self.currectSelectedPatientType.PatientTypeName);
                $("#Description").val(self.currectSelectedPatientType.Description);
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
            $('#AddEditPatientTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditPatientTypeForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditPatientTypeForm');
            var patientType = addCommonProperties(formData);
            patientType.PatientTypeId = self.currectSelectedPatientType ? self.currectSelectedPatientType.PatientTypeId : null;

            self.addeditPatientType(patientType, false);
        });

        makeFormGeneric('#AddEditPatientTypeForm', '#btnsubmit');
        self.addeditPatientType = function (patientType, iscopy) {
            makeAjaxRequest({
                url: "/PatientType/InsertOrUpdatePatientType",
                data: patientType,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditPatientTypeForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedPatientType = {};
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