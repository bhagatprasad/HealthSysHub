function DoctorAppointmentController() {
    
    var self = this;

    self.selectedRows = [];

    self.currectSelectedDoctorAppointment = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedDoctorAppointments = [];

    self.ApplicationUser = {};

    self.HospitalDoctors = [];

    var appUserInfo = storageService.get('ApplicationUser');

    if (appUserInfo)
        self.ApplicationUser = appUserInfo;

    var doctors = storageService.get('doctors');
    if (doctors) {
        self.HospitalDoctors = doctors;
    }

    const hospitalId = self.ApplicationUser.HospitalId;

    const dateTime = new Date().toISOString();

    self.init = function () {

        genarateDropdown("DoctorId", self.HospitalDoctors, "DoctorId", "FullName");

        genarateDropdown("PaymentType", paymentTypes, "PaymentTypeName", "PaymentTypeName");

        genarateDropdown("Status", hospitalStatuses, "StatusCode", "StatusName");

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
                {
                    title: "Sr No",
                    field: "TokenNo",
                    "width": 100
                },
                {
                    title: "Patient Info",
                    field: "PatientName",
                    "formatter": patientInfoFormatter
                },
                {
                    title: "Doctor",
                    field: "DoctorId",
                    "formatter": function (value, row, index) {
                        const doctor = self.HospitalDoctors.find(doc => doc.DoctorId === value._cell.row.data.DoctorId);

                        if (doctor) {
                            return `
                                    <div>
                                        <i class="fas fa-user-md"></i> ${doctor.FullName}
                                    </div>
                                `;
                        } else {
                            return '<div>N/A</div>'; 
                        }
                    }
                },
                {
                    title: "Appointment Date",
                    field: "AppointmentDate",
                    "formatter": appointmentFormatter
                },
                { title: "Amount", field: "Amount", formatter: "money", formatterParams: { symbol: "$" } },
                { title: "Payment Type", field: "PaymentType" },
                { title: "Payment Reference", field: "PaymentReference" },
                { title: "Status", field: "Status" },
                {
                    "title": "Actions",
                    "field": "actions",
                    "width": 110,
                    "formatter": customActionsFormatter,
                    "align": "center"
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
        function patientInfoFormatter(value, row, index) {
            const patientName = value._cell.row.data.PatientName ? value._cell.row.data.PatientName : 'N/A';
            const patientPhone = value._cell.row.data.PatientPhone ? value._cell.row.data.PatientPhone : 'N/A';
            const comingFrom = value._cell.row.data.ComingFrom ? value._cell.row.data.ComingFrom : 'N/A';

            return `
        <div title="${value._cell.row.data.HealthIssue}">
            <i class="fas fa-user"></i> ${patientName}<br>
            <i class="fas fa-phone"></i> ${patientPhone}<br>
            <i class="fas fa-map-marker-alt"></i> ${comingFrom}
        </div>
    `;
        }
        function appointmentFormatter(value, row, index) {
            return `
        <div>
            ${value._cell.row.data.AppointmentDate} <br>
            ${value._cell.row.data.AppointmentTime}
        </div>
    `;
        }
        function customActionsFormatter(value, row, index) {
            return `
                        <div class="btn-group" role="group">
                            <button type="button" class="btn btn-primary" onclick="printPaymentReceipt('${value._cell.row.data.AppointmentId}')">
                                <i class="fas fa-receipt"></i> Receipt
                            </button>
                        </div>
                    `;
        }
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
                $("#DoctorId").val(self.currectSelectedDoctorAppointment.DoctorId);
                $("#AppointmentDate").val(self.currectSelectedDoctorAppointment.AppointmentDate);
                $("#AppointmentTime").val(self.currectSelectedDoctorAppointment.AppointmentTime);
                $("#PatientName").val(self.currectSelectedDoctorAppointment.PatientName);
                $("#PatientPhone").val(self.currectSelectedDoctorAppointment.PatientPhone);
                $("#ComingFrom").val(self.currectSelectedDoctorAppointment.ComingFrom);
                $("#Amount").val(self.currectSelectedDoctorAppointment.Amount);
                $("#PaymentType").val(self.currectSelectedDoctorAppointment.PaymentType);
                $("#PaymentReference").val(self.currectSelectedDoctorAppointment.PaymentReference);
                $("#HealthIssue").val(self.currectSelectedDoctorAppointment.HealthIssue);
                $("#Status").val(self.currectSelectedDoctorAppointment.Status);
                $("#StatusMessage").val(self.currectSelectedDoctorAppointment.StatusMessage);
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
            showLoader();
            e.preventDefault();
            var formData = getFormData('#AddEditDoctorAppointmentForm');
            var doctorAppointment = addCommonProperties(formData);
            doctorAppointment.DoctorAppointmentId = self.currectSelectedDoctorAppointment ? self.currectSelectedDoctorAppointment.DoctorAppointmentId : null;
            doctorAppointment.HospitalId = self.ApplicationUser.HospitalId;
            doctorAppointment.TokenNo = self.currectSelectedDoctorAppointment ? self.currectSelectedDoctorAppointment.TokenNo : 0;
            self.addeditDoctorAppointment(doctorAppointment, false);
        });

        makeFormGeneric('#AddEditDoctorAppointmentForm', '#btnsubmit');
        self.addeditDoctorAppointment = function (doctorAppointment, iscopy) {
            makeAjaxRequest({
                url: "/DoctorAppointment/InsertOrUpdateDoctorAppointment",
                data: doctorAppointment,
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
                    hideLoader();
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                    hideLoader();
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