function DoctorAppointmentController() {
    var self = this;

    self.selectedRows = [];

    self.currectSelectedDoctorAppointment = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedDoctorAppointments = [];

    self.ApplicationUser = {};

    var appUserInfo = storageService.get('ApplicationUser');
    if (appUserInfo)
        self.ApplicationUser = appUserInfo;

    const hospitalId = self.ApplicationUser.HospitalId;

    const dateTime = new Date().toISOString();

    self.init = function () {
        var table = new Tabulator("#DoctorAppointmentGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: "/DoctorAppointment/GetDoctorAppointments",
            ajaxParams: {
                hospitalId: hospitalId,
                dateTime: dateTime
            },
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentDoctorAppointmentChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().DoctorAppointmentId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childDoctorAppointmentChkbox-${rowId}' class='childDoctorAppointmentChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Token No", field: "TokenNo" },
                { title: "Patient Name", field: "PatientName" },
                { title: "Patient Phone", field: "PatientPhone" },
                { title: "Coming From", field: "ComingFrom" },
                { title: "Doctor ID", field: "DoctorId" },
                { title: "Appointment Date", field: "AppointmentDate", formatter: "datetime", formatterParams: { outputFormat: "YYYY-MM-DD" } },
                { title: "Appointment Time", field: "AppointmentTime", formatter: "time", formatterParams: { outputFormat: "HH:mm:ss" } },
                { title: "Health Issue", field: "HealthIssue" },
                { title: "Amount", field: "Amount", formatter: "money", formatterParams: { symbol: "$" } },
                { title: "Payment Type", field: "PaymentType" },
                { title: "Payment Reference", field: "PaymentReference" },
                { title: "Status", field: "Status" },
                { title: "Status Message", field: "StatusMessage" },
                { title: "Created On", field: "CreatedOn", formatter: "datetime", formatterParams: { outputFormat: "YYYY-MM-DD HH:mm:ss" } },
                { title: "Modified On", field: "ModifiedOn", formatter: "datetime", formatterParams: { outputFormat: "YYYY-MM-DD HH:mm:ss" } },
                {
                    title: "Is Active",
                    field: "IsActive",
                    formatter: "tickCross",
                    align: "center"
                }

            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentDoctorAppointmentChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().DoctorAppointmentId === changedRow.DoctorAppointmentId);

                    if (foundRow) {
                        var rowId = foundRow.getData().DoctorAppointmentId;
                        var checkbox = document.querySelector(`#childDoctorAppointmentChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedDoctorAppointment = changedRow;
                        }
                        else {
                            self.currectSelectedDoctorAppointment = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentDoctorAppointmentChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childDoctorAppointmentChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childDoctorAppointmentChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.DoctorAppointmentId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentDoctorAppointmentChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedDoctorAppointment) {
                $("#Name").val(self.currectSelectedDoctorAppointment.Name);
                $("#Code").val(self.currectSelectedDoctorAppointment.Code);
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
            $('#AddEditDoctorAppointmentForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditDoctorAppointmentForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditDoctorAppointmentForm');
            var doctorAppointment = addCommonProperties(formData);
            doctorAppointment.DoctorAppointmentId = self.currectSelectedDoctorAppointment ? self.currectSelectedDoctorAppointment.DoctorAppointmentId : null;

            self.addeditDoctorAppointment(department, false);
        });

        makeFormGeneric('#AddEditDoctorAppointmentForm', '#btnsubmit');
        self.addeditDoctorAppointment = function (department, iscopy) {
            makeAjaxRequest({
                url: API_URLS.InsertOrUpdateRoleAsync,
                data: department,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditDoctorAppointmentForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedDoctorAppointment = {};
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
                self.ImportedDoctorAppointments = importedData;
                console.log(self.ImportedDoctorAppointments);
            });
        });

        $(document).on("click", "#uploadButton", function (e) {
            if (self.ImportedTenants.length > 0) {
                makeAjaxRequest({
                    url: API_URLS.BulkInsertOrUpdateTenant,
                    data: self.ImportedTenants,
                    type: 'POST',
                    successCallback: function (response) {
                        self.ImportedDoctorAppointments = [];
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