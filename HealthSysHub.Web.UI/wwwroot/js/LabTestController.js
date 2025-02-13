function LabTestController() {
    var self = this;

    self.selectedRows = [];

    self.currectSelectedLabTest = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedLabTests = [];

    self.init = function () {
        var table = new Tabulator("#LabTestGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/LabTest/FetchLabTests',
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentLabTestChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().TestId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childLabTestChkbox-${rowId}' class='childLabTestChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Test Name", field: "TestName" },
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
                $('#parentLabTestChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().TestId === changedRow.TestId);

                    if (foundRow) {
                        var rowId = foundRow.getData().TestId;
                        var checkbox = document.querySelector(`#childLabTestChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedLabTest = changedRow;
                        }
                        else {
                            self.currectSelectedLabTest = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentLabTestChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childLabTestChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childLabTestChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.TestId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentLabTestChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedLabTest) {
                $("#Name").val(self.currectSelectedLabTest.Name);
                $("#Code").val(self.currectSelectedLabTest.Code);
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
            $('#AddEditLabTestForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditLabTestForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditLabTestForm');
            var labTest = addCommonProperties(formData);
            labTest.TestId = self.currectSelectedLabTest ? self.currectSelectedLabTest.TestId : null;

            self.addeditLabTest(labTest, false);
        });

        makeFormGeneric('#AddEditLabTestForm', '#btnsubmit');
        self.addeditLabTest = function (labTest, iscopy) {
            makeAjaxRequest({
                url: "/LabTest/InsertOrUpdateLabTest",
                data: labTest,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditLabTestForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedLabTest = {};
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