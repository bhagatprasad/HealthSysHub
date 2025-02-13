function MedicineController() {

    var self = this;

    self.selectedRows = [];

    self.currectSelectedMedicine = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedMedicines = [];

    self.init = function () {
        var table = new Tabulator("#MedicineGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/Medicine/FetchMedicines',
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentMedicineChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().MedicineId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childMedicineChkbox-${rowId}' class='childMedicineChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Medicine Name", field: "MedicineName" },
                { title: "Generic Name", field: "GenericName" },
                { title: "Dosage Form", field: "DosageForm" },
                { title: "Strength", field: "Strength" },
                { title: "Manufacturer", field: "Manufacturer" },
                { title: "Batch Number", field: "BatchNumber" },
                {
                    title: "Expiry Date",
                    field: "ExpiryDate",
                    sorter: "date", // Enable date sorting
                    align: "center"
                },
                {
                    title: "Price Per Unit",
                    field: "PricePerUnit",
                    formatter: "money", // Format as currency
                    align: "right"
                },
                {
                    title: "Stock Quantity",
                    field: "StockQuantity",
                    align: "right"
                },
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
                $('#parentMedicineChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().MedicineId === changedRow.MedicineId);

                    if (foundRow) {
                        var rowId = foundRow.getData().MedicineId;
                        var checkbox = document.querySelector(`#childMedicineChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedMedicine = changedRow;
                        }
                        else {
                            self.currectSelectedMedicine = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentMedicineChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childMedicineChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childMedicineChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.MedicineId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentMedicineChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currentSelectedMedicine) {
                // Map all properties to the form fields
                $("#MedicineName").val(self.currentSelectedMedicine.MedicineName);
                $("#GenericName").val(self.currentSelectedMedicine.GenericName);
                $("#DosageForm").val(self.currentSelectedMedicine.DosageForm);
                $("#Strength").val(self.currentSelectedMedicine.Strength);
                $("#Manufacturer").val(self.currentSelectedMedicine.Manufacturer);
                $("#BatchNumber").val(self.currentSelectedMedicine.BatchNumber);
                $("#ExpiryDate").val(self.currentSelectedMedicine.ExpiryDate ? self.currentSelectedMedicine.ExpiryDate.toISOString().split('T')[0] : ''); // Format date for input[type="date"]
                $("#PricePerUnit").val(self.currentSelectedMedicine.PricePerUnit);
                $("#StockQuantity").val(self.currentSelectedMedicine.StockQuantity);

                // Show the sidebar and add backdrop
                $('#sidebar').addClass('show');
                $('body').append('<div class="modal-backdrop fade show"></div>');
            } else {
                // Hide the sidebar and remove backdrop if no medicine is selected
                $('#sidebar').removeClass('show');
                $('.modal-backdrop').remove();
            }
        });

        $('#addBtn').on('click', function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $('#closeSidebar, .modal-backdrop').on('click', function () {
            $('#AddEditMedicineForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditMedicineForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditMedicineForm');
            var medicine = addCommonProperties(formData);
            medicine.MedicineId = self.currectSelectedMedicine ? self.currectSelectedMedicine.MedicineId : null;

            self.addeditMedicine(paymentType, false);
        });

        makeFormGeneric('#AddEditMedicineForm', '#btnsubmit');
        self.addeditMedicine = function (paymentType, iscopy) {
            makeAjaxRequest({
                url: "/Medicine/InsertOrUpdateMedicine",
                data: paymentType,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditMedicineForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedMedicine = {};
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