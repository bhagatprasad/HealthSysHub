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


}