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

        //// Add responsive behavior
        window.addEventListener('resize', updateTableColumns);

        // Initialize the table
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
            columns: getColumnConfig(window.innerWidth <= 768),
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentDoctorAppointmentChkbox').prop('checked', allSelected);
                disableAllButtons();

                if (rows.length > 0) {
                    enableButtons(table);
                }

                let currentSelectedRows = rows.map(row => row.getData());
                let changedRow = null;

                if (self.selectedRows.length > currentSelectedRows.length) {
                    changedRow = self.selectedRows.find(row => !currentSelectedRows.includes(row));
                } else if (self.selectedRows.length < currentSelectedRows.length) {
                    changedRow = currentSelectedRows.find(row => !self.selectedRows.includes(row));
                }

                self.selectedRows = currentSelectedRows;
                if (changedRow) {
                    var rows = table.getRows();
                    var foundRow = rows.find(row => row.getData().AppointmentId === changedRow.AppointmentId);

                    if (foundRow) {
                        var rowId = foundRow.getData().AppointmentId;
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

        // Define column configurations
        function getColumnConfig(isMobile) {
            const baseColumns = [
                // Checkbox column (always visible)
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentDoctorAppointmentChkbox'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().AppointmentId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childDoctorAppointmentChkbox-${rowId}' class='childDoctorAppointmentChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                }
            ];

            if (isMobile) {
                // Mobile configuration - compact view
                baseColumns.push({
                    title: "Patient Details",
                    field: "PatientName",
                    width: 200,
                    formatter: function (cell, formatterParams) {
                        const data = cell.getData();
                        const doctor = self.HospitalDoctors.find(doc => doc.DoctorId === data.DoctorId);

                        return `
                    <div class="mobile-patient-view">
                        <div class="patient-mobile-row">
                            <span class="mobile-label">#${data.TokenNo || ''}</span>
                            <strong>${data.PatientName || 'N/A'}</strong>
                        </div>
                        <div class="patient-mobile-row">
                            <i class="fas fa-phone"></i> ${data.PatientPhone || 'N/A'}
                        </div>
                        <div class="patient-mobile-row">
                            <i class="fas fa-user-md"></i> ${doctor?.FullName || 'N/A'}
                        </div>
                        <div class="patient-mobile-row">
                            <i class="fas fa-calendar-day"></i> ${data.AppointmentDate || ''}
                             <i class="fas fa-calendar-day"></i> ${data.AppointmentTime || ''}
                        </div>
                        <div class="patient-mobile-row">
                            <span class="status-badge">${data.Status || ''}</span>
                        </div>
                    </div>
                `;
                    }
                });
            } else {
                // Desktop configuration - full columns
                baseColumns.push(
                    {
                        title: "Sr No",
                        field: "TokenNo",
                        width: 40
                    },
                    {
                        title: "Patient Info",
                        field: "PatientName",
                        formatter: patientInfoFormatter
                    },
                    {
                        title: "Doctor",
                        field: "DoctorId",
                        formatter: function (cell, formatterParams) {
                            const doctor = self.HospitalDoctors.find(doc => doc.DoctorId === cell.getData().DoctorId);
                            return doctor ? `<div><i class="fas fa-user-md"></i> ${doctor.FullName}</div>` : '<div>N/A</div>';
                        }
                    },
                    {
                        title: "Appointment Date",
                        field: "AppointmentDate",
                        formatter: appointmentFormatter
                    },
                    {
                        title: "Amount",
                        field: "Amount",
                        formatter: "money",
                        formatterParams: { symbol: "$" }
                    },
                    {
                        title: "Payment Type",
                        field: "PaymentType"
                    },
                    {
                        title: "Status",
                        field: "Status"
                    }
                );
            }

            // Actions column (always last and always visible)
            baseColumns.push({
                title: "Actions",
                field: "actions",
                width: isMobile ? 80 : 110,
                formatter: customActionsFormatter,
                align: "center",
                headerSort: false
            });

            return baseColumns;
        }

        // Formatter functions
        function patientInfoFormatter(cell) {
            const data = cell.getData();
            const patientName = data.PatientName || 'N/A';
            const patientPhone = data.PatientPhone || 'N/A';
            const comingFrom = data.ComingFrom || 'N/A';

            return `
                <div title="${data.HealthIssue || ''}">
                    <i class="fas fa-user"></i> ${patientName}<br>
                    <i class="fas fa-phone"></i> ${patientPhone}<br>
                    <i class="fas fa-map-marker-alt"></i> ${comingFrom}
                </div>
            `;
        }

        function appointmentFormatter(cell) {
            const data = cell.getData();
            return `
                    <div>
                        ${data.AppointmentDate || ''} <br>
                        ${data.AppointmentTime || ''}
                    </div>
                `;
        }

        function customActionsFormatter(cell) {
            const isMobile = window.innerWidth <= 768;
            const data = cell.getData();
            const appointmentId = String(data.AppointmentId).replace(/"/g, '&quot;');

            if (isMobile) {
                return `
            <div class="btn-group mobile-actions" role="group">
                <button type="button" 
                        class="btn btn-sm btn-success btn-pdf-icon pdf-print-receipt" data-appointmentid="${appointmentId}">
                    <i class="fa fa-file-pdf"></i>
                </button>
            </div>
        `;
            } else {
                return `
            <div class="btn-group desktop-actions" role="group">
                <button type="button" class="btn btn-sm btn-success btn-pdf-icon pdf-print-receipt" data-appointmentid="${appointmentId}">
                    <i class="fa fa-receipt"></i> Receipt
                </button>
            </div>
        `;
            }
        }

        // Update table columns based on screen size
        function updateTableColumns() {
            const isMobile = window.innerWidth <= 768;
            table.setColumns(getColumnConfig(isMobile));
            table.redraw(true);
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
                return data.AppointmentId === rowId;
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
            doctorAppointment.AppointmentId = self.currectSelectedDoctorAppointment ? self.currectSelectedDoctorAppointment.AppointmentId : null;
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
        $(document).on("click", ".pdf-print-receipt", function () {
            showLoader();
            var rowId = $(this).data('appointmentid');
            var row = table.getRows(function (data) {
                return data.AppointmentId === rowId;
            })[0];
            console.log(row.getData());

            $.ajax({
                url: '/Report/PrintAppointmentReceipt',
                type: 'GET',
                data: { appointmentId: rowId },
                success: function (status) {
                    window.location.href = "/Report/PrintAppointmentReceipt?appointmentId=" + rowId;
                    hideLoader();
                },
                error: function (error) {
                    console.error('Error:', error);
                    hideLoader();
                }
            });
        });

        $(document).on("click", "#exportTemplate", function () {
            showLoader();

            var searchInput = $("#searchInput").val();
            var startDate = $("#startDate").val();
            var endDate = $("#endDate").val();

            var report = {
                HospitalId: self.ApplicationUser.HospitalId,
                SearchStr: searchInput,
                FromDate: startDate ? new Date(startDate) : null,
                ToDate: endDate ? new Date(endDate) : null
            };

            $.ajax({
                url: '/Report/PrintAppointmentsReport',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(report),
                xhrFields: {
                    responseType: 'blob'
                },
                success: function (data) {
                    var blob = new Blob([data], { type: 'application/pdf' });
                    var link = document.createElement('a');
                    var url = URL.createObjectURL(blob);
                    link.href = url;
                    link.download = `AppointmentsReport_${new Date().toISOString().slice(0, 19).replace(/[:T]/g, '-')}.pdf`;
                    document.body.appendChild(link);
                    link.click();

                    // Clean up
                    setTimeout(function () {
                        document.body.removeChild(link);
                        window.URL.revokeObjectURL(url);
                        hideLoader();
                    }, 100);
                },
                error: function (error) {
                    console.error('Error:', error);
                    hideLoader();
                    alert('Error generating report. Please try again.');
                }
            });
        });

        $(document).on("click", "#searchButton", function () {
            showLoader();
            var searchInput = $("#searchInput").val();
            var startDate = $("#startDate").val();
            var endDate = $("#endDate").val();

            var doctorAppointment = {
                HospitalId: self.ApplicationUser.HospitalId,
                SearchStr: searchInput,
                FromDate: startDate ? new Date(startDate) : null,
                ToDate: endDate ? new Date(endDate) : null
            };

            makeAjaxRequest({
                url: "/DoctorAppointment/GetAppointmentsReports",
                data: doctorAppointment,
                type: 'POST',
                successCallback: function (response) {
                    console.info(response);
                    if (response) {
                        table.setData(response.data);
                    }
                    hideLoader();
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                    hideLoader();
                }
            });
        });
    };
}