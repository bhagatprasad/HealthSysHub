function LabTestInventoryController() {
    var self = this;
    self.table = null;
    self.CurrentSelectedTest = null;
    self.parentController = null;
    self.init = function (context) {
        // Store reference to parent controller and any passed data
        if (context) {
            self.parentController = context;
        }

        const calculateTableHeight = () => {
            const modalContentHeight = $('.modal-content').height();
            return modalContentHeight ? `calc(${modalContentHeight}px - 250px)` : '400px';
        };

        // Initialize the table
        self.table = new Tabulator("#labTestTable", {
            height: calculateTableHeight(),
            layout: "fitColumns",
            responsiveLayout: "collapse",
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
                { title: "Name", field: "TestName" },
                { title: "Description", field: "TestDescription" }
            ],
            rowClick: function (e, row) {
                const data = row.getData();
                self.showSelectedLabTest(data);
            }
        });

        // Search functionality
        $("#searchLabTestInput").on("input", function () {
            var value = $(this).val().toLowerCase();
            self.table.setFilter([
                { field: "TestName", type: "like", value: value },
                { field: "TestDescription", type: "like", value: value }
            ]);
        });

        // Handle window resize
        $(window).resize(function () {
            self.table.setHeight(calculateTableHeight());
            self.table.redraw(true);
        });

        // Modal show event to properly calculate heights
        $('#LabTestInventoryModal').on('shown.bs.modal', function () {
            self.table.setHeight(calculateTableHeight());
            self.table.redraw(true);
        });
    };

    // Show selected lab test in the form
    self.showSelectedLabTest = function (data) {
        console.log(data);
        self.CurrentSelectedTest = data;
        $("#selectedTestName").val(data.TestName);
        $("#selectedDescription").val(data.TestDescription || 'N/A');
        $("#selectedLabTestForm").show();

        // On mobile, scroll to the form
        if (window.innerWidth <= 768) {
            $('html, body').animate({
                scrollTop: $("#selectedLabTestForm").offset().top
            }, 500);
        }

        // Adjust table height when form is shown
        self.table.setHeight(calculateTableHeight());
    };

    // Helper function to calculate table height
    function calculateTableHeight() {
        const modalContent = $('.modal-content');
        if (modalContent.length === 0) return '400px';

        const modalHeight = modalContent.height();
        const formHeight = $("#selectedLabTestForm").is(":visible") ? $("#selectedLabTestForm").outerHeight() : 0;
        const otherElementsHeight = 200; // Approximate height of other elements

        return `calc(${modalHeight}px - ${otherElementsHeight + formHeight}px)`;
    }

    // Add lab test to cart
    $(document).on("click", "#addToLabCart", function () {
        showLoader();

        // Prepare data to send, including ConsultationDetails if available
        var labOrderRequestDetails = self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.labOrderRequestDetails;
        if (labOrderRequestDetails.LabOrderRequestId) {
            labOrderRequestDetails.DoctorName = self.parentController.ConsultedDoctor.FullName;
            labOrderRequestDetails.HospitalId = self.parentController.ConsultedDoctor.HospitalId;
            labOrderRequestDetails.HospitalName = self.parentController.ApplicationUser.HospitalName;
            labOrderRequestDetails.Name = self.parentController.ConsultationDetails.patientDetails.Name;
            labOrderRequestDetails.Phone = self.parentController.ConsultationDetails.patientDetails.Phone;
            labOrderRequestDetails.PatientId = self.parentController.ConsultationDetails.patientDetails.PatientId;
            labOrderRequestDetails.Notes = "Lab Test are requested";
            labOrderRequestDetails.Status = "SentForTests";
            labOrderRequestDetails.PatientPrescriptionId = self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.PatientPrescriptionId;
            labOrderRequestDetails = addCommonProperties(labOrderRequestDetails);
        } else {
            labOrderRequestDetails.DoctorName = self.parentController.ConsultedDoctor.FullName;
            labOrderRequestDetails.HospitalId = self.parentController.ConsultedDoctor.HospitalId;
            labOrderRequestDetails.HospitalName = self.parentController.ApplicationUser.HospitalName;
            labOrderRequestDetails.Name = self.parentController.ConsultationDetails.patientDetails.Name;
            labOrderRequestDetails.Phone = self.parentController.ConsultationDetails.patientDetails.Phone;
            labOrderRequestDetails.PatientId = self.parentController.ConsultationDetails.patientDetails.PatientId;
            labOrderRequestDetails.Notes = "Lab Test are requested";
            labOrderRequestDetails.Status = "SentForTests";
            labOrderRequestDetails.PatientPrescriptionId = self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.PatientPrescriptionId;
            labOrderRequestDetails = addCommonProperties(labOrderRequestDetails);
           
        }
        var labOrderRequestItem = {
            TestId: self.CurrentSelectedTest.TestId,
            TestName: self.CurrentSelectedTest.TestName,
            ItemQty: $("#quantity").val(),
        };

        labOrderRequestDetails.labOrderRequestItemDetails.push(addCommonProperties(labOrderRequestItem));
        console.log(labOrderRequestDetails);

        makeAjaxRequest({
            url: "/LabOrderRequest/InsertOrUpdateLabOrderRequest",
            data: labOrderRequestDetails,
            type: 'POST',
            successCallback: function (response) {
                if (response) {
                    self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.labOrderRequestDetails = response.data;
                    $('#LabTestInventoryModal').modal('hide');
                }
                hideLoader();
                self.parentController.onLabTestAdded(response.data);
            },
            errorCallback: function (xhr, status, error) {
                console.error("Error adding lab test: " + error);
                hideLoader();
            }
        });
    });
}