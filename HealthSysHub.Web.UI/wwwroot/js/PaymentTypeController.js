function PaymentTypeController() {

    var self = this;

    self.selectedRows = [];

    self.currectSelectedPaymentType = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedPaymentTypes = [];

    self.init = function () {
        var table = new Tabulator("#PaymentTypeGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/PaymentType/FetchPaymentTypes',
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentPaymentTypeChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().PaymentTypeId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childPaymentTypeChkbox-${rowId}' class='childPaymentTypeChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "PaymentType Name", field: "PaymentTypeName" },
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
                $('#parentPaymentTypeChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().PaymentTypeId === changedRow.PaymentTypeId);

                    if (foundRow) {
                        var rowId = foundRow.getData().PaymentTypeId;
                        var checkbox = document.querySelector(`#childPaymentTypeChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedPaymentType = changedRow;
                        }
                        else {
                            self.currectSelectedPaymentType = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentPaymentTypeChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childPaymentTypeChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childPaymentTypeChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.PaymentTypeId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentPaymentTypeChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedPaymentType) {
                $("#PaymentTypeName").val(self.currectSelectedPaymentType.PaymentTypeName);
                $("#Description").val(self.currectSelectedPaymentType.Description);
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
            $('#AddEditPaymentTypeForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditPaymentTypeForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditPaymentTypeForm');
            var paymentType = addCommonProperties(formData);
            paymentType.PaymentTypeId = self.currectSelectedPaymentType ? self.currectSelectedPaymentType.PaymentTypeId : null;

            self.addeditPaymentType(paymentType, false);
        });

        makeFormGeneric('#AddEditPaymentTypeForm', '#btnsubmit');
        self.addeditPaymentType = function (paymentType, iscopy) {
            makeAjaxRequest({
                url: "/PaymentType/InsertOrUpdatePaymentType",
                data: paymentType,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditPaymentTypeForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedPaymentType = {};
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