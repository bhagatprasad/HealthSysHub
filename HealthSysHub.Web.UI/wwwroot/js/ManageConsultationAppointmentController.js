function ManageConsultationAppointmentController() {

    var self = this;

    var actions = [];

    var dataObjects = [];

    self.dbLabTests = [];

    self.dbMedicines = [];

    self.ConsultationDetails = null;

    self.HospitalDoctors = [];

    self.ConsultedDoctor = null;

    self.appointmentId;

    self.init = function () {
        showLoader();

        var appUserInfo = storageService.get('ApplicationUser');

        if (appUserInfo)
            self.ApplicationUser = appUserInfo;

        var doctors = storageService.get('doctors');
        if (doctors) {
            self.HospitalDoctors = doctors;
        }
        function adjustButtonSize() {
            const $button = $('#btnSubmitPatientConsultation');

            // Remove all size classes first
            $button.removeClass('btn-lg btn-sm btn-block');

            // Check screen width and add appropriate class
            if ($(window).width() < 768) { // Mobile screens
                $button.addClass('btn-lg btn-block');
            }
            // For desktop, default button size (no class needed)
        }

        // Run on page load
        adjustButtonSize();

        // Run on window resize
        $(window).resize(function () {
            adjustButtonSize();
        });

        self.appointmentId = getQueryStringParameter("appointmentId");

        dataObjects.push({ appointmentId: self.appointmentId });

        actions.push('/LabTest/FetchLabTests');

        actions.push('/Medicine/FetchMedicines');

        actions.push('/Consultation/GetConsultationDetailsByAppointmentId');

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 2) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });
        $.when.apply($, requests).done(function () {

            var responses = arguments;

            console.log(responses);

            self.dbLabTests = responses[0][0].data ? responses[0][0].data : [];

            self.dbMedicines = responses[1][0].data ? responses[1][0].data : [];

            self.ConsultationDetails = responses[2][0].data ? responses[2][0].data : null;

            if (self.ConsultationDetails && self.ConsultationDetails.DoctorId && self.HospitalDoctors) {
                self.ConsultedDoctor = $.grep(self.HospitalDoctors, function (doctor) {
                    return doctor.DoctorId == self.ConsultationDetails.DoctorId;
                })[0] || null;
            }

            console.log(self.ConsultationDetails);

            console.log(self.ConsultedDoctor);

            self.RebuildPatientDetailsUI();

            self.RebuildDoctorDetailsUI();

            self.RebuildVitalsUI();

            self.RebuildLabordersUI();

            self.RebuildPharmacyOrdersUI();

            self.PriscriptionDetailsUI();

            hideLoader();

        }).fail(function () {
            console.log('One or more requests failed.');
            hideLoader();
        });

        self.PriscriptionDetailsUI = function () {
            var data = self.ConsultationDetails.patientDetails.patientPrescriptionDetails;

            $('#Diagnosis').val(data.diagnosis || '');
            $('#Treatment').val(data.treatment || '');
            $('#Advice').val(data.advice || '');

            // Format date for datetime-local input (remove timezone if present)
            const followUpDate = data.followUpOn ?
                data.followUpOn.split('+')[0].split('.')[0] :
                '';
            $('#FollowUpOn').val(followUpDate);
        }
        self.RebuildDoctorDetailsUI = function () {

            var doctorDetails = self.ConsultedDoctor;

            $('.doctor-info h4').text('Dr. ' + doctorDetails.FullName);

            // Set doctor meta information
            $('.doctor-meta').html(`<span class="badge badge-light mr-2"><i class="fas fa-id-card"></i> ID: ${doctorDetails.DoctorId.substring(0, 8)}</span>
            ${doctorDetails.Experience ? `<span class="badge badge-light"><i class="fas fa-award"></i> ${doctorDetails.Experience}</span>` : ''}`);

            // Set contact information
            $('.doctor-rating').before(`<div class="doctor-contact mt-2"><span class="text-muted mr-3"><i class="fas fa-phone"></i> ${doctorDetails.PhoneNumber}</span>
            <span class="text-muted"><i class="fas fa-envelope"></i> ${doctorDetails.Email}</span></div>`);
        }

        function buildLabOrdersTable(labOrders) {
            const $labOrdersSection = $('<div>').addClass('lab-orders-section mt-4');

            // Header with add button
            const $header = $('<div>').addClass('d-flex justify-content-between align-items-center mb-2');
            $header.append($('<h6>').addClass('mb-0').html('<i class="fas fa-flask mr-2"></i> Lab Orders'));
            $header.append(
                $('<button>').addClass('btn btn-sm btn-primary').html('<i class="fas fa-plus"></i> Add')
                    .click(function () {
                        $('#LabTestInventoryModal').modal({ backdrop: 'static', keyboard: true });
                        $("#LabTestInventoryModal").modal("show");
                    })
            );

            // Table for large screens
            const $table = $('<div>').addClass('d-none d-md-block table-responsive').append(
                $('<table>').addClass('table table-sm table-bordered').append(
                    $('<thead>').append(
                        $('<tr>').append(
                            $('<th>').text('Test Name'),
                            $('<th>').text('Status'),
                            $('<th>').text('Requested Date'),
                            $('<th>').text('Actions')
                        )
                    ),
                    $('<tbody>')
                )
            );

            // Cards for small screens
            const $mobileView = $('<div>').addClass('d-md-none');

            // Process each lab order
            labOrders.labOrderRequestItemDetails.forEach(order => {
                // Add to table (desktop view)
                $table.find('tbody').append(
                    $('<tr>').append(
                        $('<td>').text(order.TestName),
                        $('<td>').append(
                            $('<span>').addClass(`badge badge-${getStatusClass(labOrders.Status)}`).text(labOrders.Status)
                        ),
                        $('<td>').text(order.CreatedOn),
                        $('<td>').append(
                            $('<button>').addClass('btn btn-sm btn-outline-primary').text('View')
                                .click(function () { /* View handler */ })
                        )
                    )
                );

                // Add to mobile view
                $mobileView.append(
                    $('<div>').addClass('card mb-2').append(
                        $('<div>').addClass('card-body').append(
                            $('<div>').addClass('row').append(
                                $('<div>').addClass('col-8').append(
                                    $('<h6>').addClass('mb-1 font-weight-bold').text(order.TestName),
                                    $('<div>').addClass('d-flex flex-wrap').append(
                                        $('<small>').addClass('mr-2').html(`<strong>Status:</strong> <span class="badge badge-${getStatusClass(labOrders.Status)}">${labOrders.Status}</span>`),
                                        $('<small>').html(`<strong>Date:</strong> ${order.CreatedOn}`)
                                    )
                                ),
                                $('<div>').addClass('col-4 text-right').append(
                                    $('<button>').addClass('btn btn-sm btn-outline-danger').html('<i class="fas fa-trash"></i>')
                                        .click(function () { /* Delete handler */ })
                                )
                            )
                        )
                    )
                );
            });
          

            $labOrdersSection.append($header, $table, $mobileView);
            return $labOrdersSection;
        }

        // Helper function to get status badge class
        function getStatusClass(status) {
            switch (status.toLowerCase()) {
                case 'pending': return 'warning';
                case 'completed': return 'success';
                case 'cancelled': return 'danger';
                default: return 'secondary';
            }
        }

        // Usage:

        function buildPharmacyOrdersTable(pharmacyOrders) {
            const $pharmacySection = $('<div>').addClass('pharmacy-orders-section mt-4');

            // Header with add button
            const $header = $('<div>').addClass('d-flex justify-content-between align-items-center mb-2');
            $header.append($('<h6>').addClass('mb-0').html('<i class="fas fa-pills mr-2"></i> Pharmacy Orders'));
            $header.append(
                $('<button>').addClass('btn btn-sm btn-primary').html('<i class="fas fa-plus"></i> Add').click(function () {
                    $('#MedicineInventoryModal').modal({ backdrop: 'static', keyboard: true });
                    $("#MedicineInventoryModal").modal("show");

                })
            );

            // Table for large screens
            const $table = $('<div>').addClass('d-none d-md-block table-responsive').append(
                $('<table>').addClass('table table-sm table-bordered').append(
                    $('<thead>').append(
                        $('<tr>').append(
                            $('<th>').text('Medicine Name'),
                            $('<th>').text('Usage'),
                            $('<th>').text('ItemQty'),
                            $('<th>').text('Actions')
                        )
                    ),
                    $('<tbody>')
                )
            );

            // Cards for small screens
            const $mobileView = $('<div>').addClass('d-md-none');

            // Process each pharmacy order
            if (pharmacyOrders.pharmacyOrderRequestItemDetails.length > 0) {
                pharmacyOrders.pharmacyOrderRequestItemDetails.forEach(order => {
                    // Add to table (desktop view)
                    $table.find('tbody').append(
                        $('<tr>').append(
                            $('<td>').text(order.MedicineName),
                            $('<td>').text(order.ItemQty),
                            $('<td>').append(
                                $('<span>').addClass(`badge badge-${pharmacyOrders.Status}`).text(pharmacyOrders.Status)
                            ),
                            $('<td>').append(
                                $('<button>').addClass('btn btn-sm btn-outline-danger').html('<i class="fas fa-trash"></i>')
                                    .click(function () { /* Delete handler */ })
                            )
                        )
                    );

                    // Add to mobile view
                    $mobileView.append(
                        $('<div>').addClass('card mb-2').append(
                            $('<div>').addClass('card-body').append(
                                $('<div>').addClass('row').append(
                                    $('<div>').addClass('col-8').append(
                                        $('<h6>').addClass('mb-1 font-weight-bold').text(order.MedicineName),
                                        $('<div>').addClass('d-flex flex-wrap').append(
                                            $('<small>').addClass('mr-2').html(`<strong>Dosage:</strong> ${order.ItemQty}`),
                                            $('<small>').html(`<strong>Status:</strong> <span class="badge badge-${pharmacyOrders.Status}">${pharmacyOrders.Status}</span>`)
                                        )
                                    ),
                                    $('<div>').addClass('col-4 text-right').append(
                                        $('<button>').addClass('btn btn-sm btn-outline-danger').html('<i class="fas fa-trash"></i>')
                                            .click(function () { /* Delete handler */ })
                                    )
                                )
                            )
                        )
                    );
                });
            }
          

            $pharmacySection.append($header, $table, $mobileView);
            return $pharmacySection;
        }

        // Helper function to get status badge class
        function getPharmacyStatusClass(status) {
            switch (status.toLowerCase()) {
                case 'filled': return 'success';
                case 'pending': return 'warning';
                case 'cancelled': return 'danger';
                case 'prescribed': return 'info';
                default: return 'secondary';
            }
        }

        // Usage:

        self.RebuildPharmacyOrdersUI = function () {
            const pharmacyOrdersData = self.ConsultationDetails.patientDetails.patientPrescriptionDetails.pharmacyOrderRequestDetails;
            $('#pharmacy-orders-section').append(buildPharmacyOrdersTable(pharmacyOrdersData));
        }


        self.RebuildLabordersUI = function () {
            const labOrdersData = self.ConsultationDetails.patientDetails.patientPrescriptionDetails.labOrderRequestDetails;
            $('#lab-orders-section').append(buildLabOrdersTable(labOrdersData));
        }


        self.RebuildVitalsUI = function () {
            var vitals = self.ConsultationDetails.patientDetails.patientVitalDetails;

            $('#BodyTemperature').text(vitals.BodyTemperature + ' °F');
            $('#HeartRate').text(vitals.HeartRate + ' bpm');
            $('#BloodPressure').text(vitals.BloodPressure + ' mmHg');
            $('#RespiratoryRate').text(vitals.RespiratoryRate + ' rpm');
            $('#OxygenSaturation').text(vitals.OxygenSaturation + '%');
            $('#Height').text(vitals.Height + ' cm');
            $('#Weight').text(vitals.Weight + ' kg');
            $('#BMI').text(vitals.BMI);

            $('.vitals-card textarea').val(vitals.Notes || '');
        };
        self.RebuildPatientDetailsUI = function () {
            var patientDetails = self.ConsultationDetails.patientDetails;
            $('.patient-info h4').text(patientDetails.Name);
            $('.patient-meta').html(`
        <span class="badge badge-light mr-2"><i class="fas fa-id-card"></i> MRN: ${patientDetails.PatientId.substring(0, 8)}</span>
        <span class="badge badge-light mr-2"><i class="fas fa-birthday-cake"></i> ${patientDetails.Age} years</span>
        <span class="badge badge-light"><i class="fas fa-venus-mars"></i> ${patientDetails.Gender}</span>
            `);
            $('.patient-contact').html(`
        <span class="text-muted mr-3"><i class="fas fa-phone"></i> ${patientDetails.Phone}</span>
        <span class="text-muted"><i class="fas fa-map-marker-alt"></i> ${patientDetails.Address}</span>
    `);
        };

        self.onLabTestAdded = function (labTestResponse) {
            showLoader();
            $('#lab-orders-section').html("");
            console.log("Lab test added:", labTestResponse);
            self.ConsultationDetails.patientDetails.patientPrescriptionDetails.labOrderRequestDetails = labTestResponse;
            self.RebuildLabordersUI();
           
            hideLoader();
        };

        self.onMedicineAdded = function (medicineResponse) {
            showLoader();
            $('#pharmacy-orders-section').html("");
            console.log("medicine added:", medicineResponse);
            self.ConsultationDetails.patientDetails.patientPrescriptionDetails.pharmacyOrderRequestDetails = medicineResponse;
            self.RebuildPharmacyOrdersUI();

            hideLoader();
        };
    };

};