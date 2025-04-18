function MedicineInventoryController() {
    var self = this;
    self.table = null;
    self.CurrentSelectedMedicine = null;
    self.parentController = null;
    self.init = function (context) {
        if (context) {
            self.parentController = context;
        }
        const calculateTableHeight = () => {
            const modalContentHeight = $('.modal-content').height();
            return modalContentHeight ? `calc(${modalContentHeight}px - 250px)` : '400px';
        };

        // Initialize the table
        self.table = new Tabulator("#medicineTable", {
            height: calculateTableHeight(),
            layout: "fitColumns",
            responsiveLayout: "collapse",
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
                { title: "Name", field: "MedicineName" },
                { title: "Dosage", field: "DosageForm", visible: window.innerWidth > 768 },
                { title: "Strength", field: "Strength" },
                { title: "Brand", field: "Manufacturer", visible: window.innerWidth > 768 },
                {
                    title: "Price",
                    field: "PricePerUnit",
                    formatter: "money",
                    align: "right",
                    formatterParams: { symbol: "$", precision: 2 }
                },
                {
                    title: "Stock",
                    field: "StockQuantity",
                    align: "right"
                }
            ],
            rowClick: function (e, row) {
                const data = row.getData();
                self.showSelectedMedicine(data);
            }
        });

        // Search functionality
        $("#searchInput").on("input", function () {
            var value = $(this).val().toLowerCase();
            self.table.setFilter([
                { field: "MedicineName", type: "like", value: value },
                { field: "Manufacturer", type: "like", value: value }
            ]);
        });

        // Handle window resize
        $(window).resize(function () {
            self.table.setHeight(calculateTableHeight());
            self.table.redraw(true);
        });

        // Modal show event to properly calculate heights
        $('#MedicineInventoryModal').on('shown.bs.modal', function () {
            self.table.setHeight(calculateTableHeight());
            self.table.redraw(true);
        });
    };

    // Show selected medicine in the form
    self.showSelectedMedicine = function (data) {
        self.CurrentSelectedMedicine = data;
        $("#selectedMedicineName").val(data.MedicineName);
        $("#selectedGenericName").val(data.GenericName || 'N/A');
        $("#selectedMedicineForm").show();

        // On mobile, scroll to the form
        if (window.innerWidth <= 768) {
            $('html, body').animate({
                scrollTop: $("#selectedMedicineForm").offset().top
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
        const formHeight = $("#selectedMedicineForm").is(":visible") ? $("#selectedMedicineForm").outerHeight() : 0;
        const otherElementsHeight = 200; // Approximate height of other elements

        return `calc(${modalHeight}px - ${otherElementsHeight + formHeight}px)`;
    }

    $(document).on("click", "#addToCart", function () {
        showLoader();
        // Prepare data to send, including ConsultationDetails if available
        var pharmacyOrderRequestDetails = self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.pharmacyOrderRequestDetails;

        // Set common properties whether new or existing request
        pharmacyOrderRequestDetails.DoctorName = self.parentController.ConsultedDoctor.FullName;
        pharmacyOrderRequestDetails.HospitalId = self.parentController.ConsultedDoctor.HospitalId;
        pharmacyOrderRequestDetails.HospitalName = self.parentController.ApplicationUser.HospitalName;
        pharmacyOrderRequestDetails.Name = self.parentController.ConsultationDetails.patientDetails.Name;
        pharmacyOrderRequestDetails.Phone = self.parentController.ConsultationDetails.patientDetails.Phone;
        pharmacyOrderRequestDetails.PatientId = self.parentController.ConsultationDetails.patientDetails.PatientId;
        pharmacyOrderRequestDetails.Notes = "Medicines are requested";
        pharmacyOrderRequestDetails.Status = "SentForPharmacy";
        pharmacyOrderRequestDetails.PatientPrescriptionId = self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.PatientPrescriptionId;
        pharmacyOrderRequestDetails.PharmacyOrderRequestId = self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.pharmacyOrderRequestDetails.PharmacyOrderRequestId == "00000000-0000-0000-0000-000000000000" ? null : self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.pharmacyOrderRequestDetails.PharmacyOrderRequestId;
        // Add common properties (created/modified info)
        pharmacyOrderRequestDetails = addCommonProperties(pharmacyOrderRequestDetails);

        // Create the medicine order item
        var pharmacyOrderRequestItem = {
            MedicineId: self.CurrentSelectedMedicine.MedicineId,
            MedicineName: self.CurrentSelectedMedicine.MedicineName,
            HospitalId: self.parentController.ConsultedDoctor.HospitalId,
            ItemQty: $("#quantity").val(),
            Usage: $("#usage").val()
        };

        // Add the item to the request
        if (!pharmacyOrderRequestDetails.pharmacyOrderRequestItemDetails) {
            pharmacyOrderRequestDetails.pharmacyOrderRequestItemDetails = [];
        }
        pharmacyOrderRequestDetails.pharmacyOrderRequestItemDetails.push(addCommonProperties(pharmacyOrderRequestItem));

        console.log(pharmacyOrderRequestDetails);

        makeAjaxRequest({
            url: "/PharmacyOrderRequest/InsertOrUpdatePharmacyOrderRequestDetails",
            data: pharmacyOrderRequestDetails,
            type: 'POST',
            successCallback: function (response) {
                if (response) {
                    // Update the parent controller with the response
                    self.parentController.ConsultationDetails.patientDetails.patientPrescriptionDetails.pharmacyOrderRequestDetails = response.data;
                    $('#MedicineInventoryModal').modal('hide');
                }
                hideLoader();
                // Notify parent controller if callback exists
                if (self.parentController && typeof self.parentController.onMedicineAdded === 'function') {
                    self.parentController.onMedicineAdded(response.data);
                }
            },
            errorCallback: function (xhr, status, error) {
                console.error("Error adding medicine: " + error);
                hideLoader();
            }
        });
    });
}