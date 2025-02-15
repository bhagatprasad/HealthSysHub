function ManageHospitalController() {

    var self = this;

    self.hospitalId = null;

    self.dbHospitalTypes = [];

    self.dbDepartments = [];

    self.dbSpecializations = [];

    self.ApplicationUser = {};

    self.HospitalInformation = {};

    self.hospitalContactInformation = [];

    self.hospitalContentInformation = {};

    self.hospitalDepartmentInformation = [];

    self.hospitalSpecialtyInformation = [];

    self.hospitalMainInformation = {};

    self.HospitalInformation.hospitalContactInformation = self.hospitalContactInformation;

    self.HospitalInformation.hospitalContentInformation = self.hospitalContentInformation;

    self.HospitalInformation.hospitalDepartmentInformation = self.hospitalDepartmentInformation;

    self.HospitalInformation.hospitalSpecialtyInformation = self.hospitalSpecialtyInformation;

    var actions = [];

    var dataObjects = [];

    self.todayDate = new Date();

    actions.push("/Hospital/FetchHospitalInformationById");

    actions.push('/HospitalType/FetchHospitalTypes');

    actions.push('/Department/FetchDepartments');

    actions.push('/Specialization/FetchSpecializations');

    var appuser = storageService.get("ApplicationUser");

    if (appuser) {
        self.ApplicationUser = appuser;
    }

    self.hospitalId = getQueryStringParameter("hospitalId");

    self.init = function () {

        showLoader();

        dataObjects.push({ hospitalId: self.hospitalId });


        var requests = actions.map((action, index) => {

            var ajaxConfig = {
                url: action,
                method: 'GET'
            };

            if (index === 0) {
                ajaxConfig.data = dataObjects[0];

            }
            return $.ajax(ajaxConfig);
        }); $.when.apply($, requests).done(function (...responses) {

            self.HospitalInformation = responses[0][0]?.data || {};

            self.dbHospitalTypes = responses[1][0]?.data || [];

            self.dbDepartments = responses[2][0]?.data || [];

            self.dbSpecializations = responses[3][0]?.data || [];

            genarateDropdown("HospitalTypeId", self.dbHospitalTypes, "HospitalTypeId", "HospitalTypeName");

            genarateDropdown("SpecializationId", self.dbSpecializations, "SpecializationId", "SpecializationName");

            if (self.HospitalInformation) {
                // map to main info
                var hospitalInformation = self.HospitalInformation;

                self.hospitalMainInformation = {
                    HospitalId: hospitalInformation.HospitalId,
                    HospitalName: hospitalInformation.HospitalName,
                    HospitalCode: hospitalInformation.HospitalCode,
                    RegistrationNumber: hospitalInformation.RegistrationNumber,
                    Address: hospitalInformation.Address,
                    City: hospitalInformation.City,
                    State: hospitalInformation.State,
                    Country: hospitalInformation.Country,
                    PostalCode: hospitalInformation.PostalCode,
                    PhoneNumber: hospitalInformation.PhoneNumber,
                    Email: hospitalInformation.Email,
                    Website: hospitalInformation.Website,
                    LogoUrl: hospitalInformation.LogoUrl,
                    HospitalTypeId: hospitalInformation.HospitalTypeId,
                    HospitalTypeName: hospitalInformation.HospitalTypeName,
                    CreatedBy: hospitalInformation.CreatedBy,
                    CreatedOn: hospitalInformation.CreatedOn,
                    ModifiedBy: hospitalInformation.ModifiedBy,
                    ModifiedOn: hospitalInformation.ModifiedOn,
                    IsActive: hospitalInformation.IsActive
                };

                //map contact info
                self.hospitalContactInformation = hospitalInformation.hospitalContactInformation;

                //map content info
                self.hospitalContentInformation = hospitalInformation.hospitalContentInformation;

                //map department info
                self.hospitalDepartmentInformation = hospitalInformation.hospitalDepartmentInformation;

                //map speciality info
                self.hospitalSpecialtyInformation = hospitalInformation.hospitalSpecialtyInformation;
            }
            self.PrepareHospitalMainInformationUI();

            self.PrepareHospitalContentInformationUI();

            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        self.PrepareHospitalMainInformationUI = function () {
            if (self.hospitalMainInformation) {
                // Assign values to the form elements
                $("#HospitalName").val(self.hospitalMainInformation.HospitalName);
                $("#HospitalCode").val(self.hospitalMainInformation.HospitalCode);
                $("#RegistrationNumber").val(self.hospitalMainInformation.RegistrationNumber);
                $("#Address").val(self.hospitalMainInformation.Address);
                $("#City").val(self.hospitalMainInformation.City);
                $("#State").val(self.hospitalMainInformation.State);
                $("#Country").val(self.hospitalMainInformation.Country);
                $("#PostalCode").val(self.hospitalMainInformation.PostalCode);
                $("#PhoneNumber").val(self.hospitalMainInformation.PhoneNumber);
                $("#Email").val(self.hospitalMainInformation.Email);
                $("#Website").val(self.hospitalMainInformation.Website);
                $("#LogoUrl").val(self.hospitalMainInformation.LogoUrl);
                $("#HospitalTypeId").val(self.hospitalMainInformation.HospitalTypeId);

                var logoUrl = self.hospitalMainInformation.LogoUrl || "https://www.apollohospitals.com/hyderabad/wp-content/themes/apollo/assets-v3/images/logo.svg";
                $("#HospitalLogo").attr("src", logoUrl);

                // Set the hospital name
                $("#HospitalNameDisplay").text(self.hospitalMainInformation.HospitalName || "Hospital Name");

                // Set the registration number
                $("#RegistrationNumberDisplay").text("Registration Number: " + (self.hospitalMainInformation.RegistrationNumber || "N/A")); 
            }
        };
        self.PrepareHospitalContactInformationUI = function () {
           
        };

        self.PrepareHospitalContentInformationUI = function () {
            if (self.hospitalContactInformation) {
                $("#Description").text(self.hospitalContactInformation.Description || "Hospital Name");
            }
        };
        self.PrepareHospitalDepartmentInformationUI = function () {

        };

        self.PrepareHospitalSpecialtyInformationUI = function () {

        };
        $(document).on("change", "#SpecializationId",function () {
            var selectedValue = $(this).val();
            var selectedText = $(this).find("option:selected").text();

            // Check if a valid option is selected
            if (selectedValue) {
                // Create a new pill
                var pill = $('<div class="badge badge-primary m-1 pill">' + selectedText +
                    ' <span class="close" style="cursor:pointer;">&times;</span></div>');

                // Append the pill to the pills container
                $("#pillsContainer").append(pill);

                // Clear the selected option from the dropdown
                $(this).val(""); // Reset the dropdown selection
            }
        });
        $(document).on("click", ".close", function () {
            var pillText = $(this).parent(".pill").text().trim(); // Get the text of the pill
            var pillValue = $("#SpecializationId option").filter(function () {
                return $(this).text() === pillText; 
            }).val();

            // Remove the pill
            $(this).parent(".pill").remove();
            l// Add the option back to the dropdown
            $("#SpecializationId").append('<option value="' + pillValue + '">' + pillText + '</option>');
        });
    };
}