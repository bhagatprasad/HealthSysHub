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

            genarateDropdown("DepartmentId", self.dbDepartments, "DepartmentId", "DepartmentName");

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

            self.PrepareHospitalContactInformationUI();

            self.PrepareHospitalSpecialtyInformationUI();


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
            var hospitalContactGrid = $("#HospitalContactGrid");
            hospitalContactGrid.html("");

            if (self.hospitalContactInformation) {
                self.hospitalContactInformation.forEach(function (item) {
                    var trRow = $("<tr></tr>");
                    trRow.append($("<td></td>").text(item.ContactType || 'N/A'));
                    trRow.append($("<td></td>").text(item.Email || 'N/A')); // Email
                    trRow.append($("<td></td>").text(item.Phone || 'N/A')); // Phone

                    var actionsCell = $("<td class='text-center'></td>");
                    var deleteLink = $("<a href='#' class='link-primary link-delete link-delete-contact' data-hospitalContactId='" + item.HospitalContactId + "'><i class='fa fa-trash-o' style='color: red;' data-hospitalContactId='" + item.HospitalContactId + "'></i></a>");
                    actionsCell.append(deleteLink);

                    trRow.append(actionsCell);

                    hospitalContactGrid.append(trRow);
                });
            }
        };

        self.PrepareHospitalContentInformationUI = function () {
            if (self.hospitalContentInformation) {
                $("#Description").text(self.hospitalContentInformation.Description || "Hospital Name");
            }
        };
        self.PrepareHospitalDepartmentInformationUI = function () {

        };

        self.PrepareHospitalSpecialtyInformationUI = function () {
            var pillsContainer = $("#pillsContainer");
            pillsContainer.html(""); 
            if (self.hospitalSpecialtyInformation && self.hospitalSpecialtyInformation.length > 0) {

                self.hospitalSpecialtyInformation.forEach(function (item) {
                    var pill = $('<div class="badge badge-primary m-1 pill" data-hospitalSpecialtyId=' + item.HospitalSpecialtyId + '>' + item.SpecializationName +
                        ' <span class="close" style="cursor:pointer;">&times;</span></div>');
                    $("#pillsContainer").append(pill);
                });
            }
        };
        $(document).on("click", ".link-delete-contact", function (e) {
            e.preventDefault();

            var hospitalContactId = $(this).data("hospitalcontactid");

            var hospitalContactDeleteItem = self.hospitalContactInformation.filter(x => x.HospitalContactId === hospitalContactId)[0];

            if (hospitalContactDeleteItem) {
                if (confirm("Are you sure you want to delete this contact?")) {
                    console.log(hospitalContactDeleteItem);
                    hospitalContactDeleteItem.HospitalId = self.hospitalId;
                    hospitalContactDeleteItem.IsActive = false;
                    hospitalContactDeleteItem.ModifiedBy = self.ApplicationUser.Id;
                    hospitalContactDeleteItem.ModifiedOn = new Date();
                    self.InsertOrUpdateHospitalContactInformationAsync(hospitalContactDeleteItem);
                }
            } else {
                alert("Contact not found.");
            }
        });

        // Function to rebind the hospital contact grid
        function bindHospitalContactGrid() {
            var hospitalContactGrid = $("#HospitalContactGrid");
            hospitalContactGrid.html(""); // Clear existing rows

            if (self.hospitalContactInformation) {
                self.hospitalContactInformation.forEach(function (item) {
                    var trRow = $("<tr></tr>");
                    trRow.append($("<td></td>").text(item.ContactType || 'N/A'));
                    trRow.append($("<td></td>").text(item.Email || 'N/A'));
                    trRow.append($("<td></td>").text(item.Phone || 'N/A'));

                    var actionsCell = $("<td class='text-center'></td>");
                    var deleteLink = $("<a href='#' class='link-primary link-delete link-delete-contact' data-hospitalContactId='" + item.HospitalContactId + "'><i class='fa fa-trash-o' style='color: red;'></i></a>");
                    actionsCell.append(deleteLink);

                    trRow.append(actionsCell);
                    hospitalContactGrid.append(trRow);
                });
            }
        }
        $(document).on("click", "#contactSubmitButton", function (hopitalContact) {
            showLoader();
            var contactType = $("#ContactType").val();
            var contactEmail = $("#ContactEmail").val();
            var contactPhone = $("#ContactPhone").val();
            var hopitalContact = {
                HospitalContactId: null,
                HospitalId: self.hospitalId,
                ContactType: contactType,
                Phone: contactPhone,
                Email: contactEmail
            };
            var hospitalContactInformation = addCommonProperties(hopitalContact);
            self.InsertOrUpdateHospitalContactInformationAsync(hospitalContactInformation);

        });
        self.InsertOrUpdateHospitalContactInformationAsync = function (hopitalContact) {
            showLoader();
            makeAjaxRequest({
                url: "/Hospital/InsertOrUpdateHospitalContactInformation",
                data: hopitalContact,
                type: 'POST',
                successCallback: function (response) {
                    self.hospitalContactInformation = response && response.data ? response.data : [];
                    var _hospitalContactGrid = $("#HospitalContactGrid");
                    _hospitalContactGrid.html("");
                    $("#ContactType").prop("selectedIndex", 0);
                    $("#ContactEmail").val("");
                    $("#ContactPhone").val("");
                    self.PrepareHospitalContactInformationUI();
                    console.info(response);
                    hideLoader();
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                    hideLoader();
                }
            });
        };
        $(document).on("change", "#SpecializationId", function () {
            var selectedValue = $(this).val();
            var selectedText = $(this).find("option:selected").text();

            // Check if a valid option is selected
            if (selectedValue) {
                var hospitalSpecialtyInformation = {
                    HospitalSpecialtyId: null,
                    HospitalId: self.hospitalId,
                    SpecializationId: selectedValue,
                    SpecializationName: selectedText
                };
                var hospitalSpecialty = addCommonProperties(hospitalSpecialtyInformation);
                self.addEditHospitalSpecializationAsync(hospitalSpecialty);
            }
        });

        self.addEditHospitalSpecializationAsync = function (specialization) {
            makeAjaxRequest({
                url: "/Hospital/InsertOrUpdateHospitalSpecialtyInformation",
                data: specialization,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        self.hospitalSpecialtyInformation = response && response.data ? response.data : [];
                    }
                    $("#SpecializationId").prop("selectedIndex", 0);
                    self.PrepareHospitalSpecialtyInformationUI();
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });
        };
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

        $(document).on("change", "#DepartmentId", function () {
            var selectedValue = $(this).val();
            var selectedText = $(this).find("option:selected").text();

            // Check if a valid option is selected
            if (selectedValue) {
                // Create a new pill
                var pill = $('<div class="badge badge-primary m-1 pill">' + selectedText +
                    ' <span class="close" style="cursor:pointer;">&times;</span></div>');

                // Append the pill to the pills container
                $("#pillsDepartmentContainer").append(pill);

                // Clear the selected option from the dropdown
                $(this).val(""); // Reset the dropdown selection
            }
        });
        $('#AddEditHospitalForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditHospitalForm');
            var hospital = addCommonProperties(formData);
            hospital.HospitalId = self.hospitalId;

            self.addeditHospital(hospital, false);
        });

        makeFormGeneric('#AddEditHospitalForm', '#btnsubmit');
        self.addeditHospital = function (hospital, iscopy) {
            makeAjaxRequest({
                url: "/Hospital/InsertOrUpdateHospital",
                data: hospital,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        self.hospitalMainInformation = response.data;
                    }
                    self.PrepareHospitalMainInformationUI();
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });
        };
        $(document).on("click", "#btncontentsubmit", function () {
            showLoader();
            var hospitalContent = {
                HospitalContentId: self.hospitalContentInformation && self.hospitalContentInformation.HospitalContentId ? self.hospitalContentInformation.HospitalContentId : null,
                HospitalId: self.hospitalId,
                Description: $("#Description").val(),
            };
            var hospitalContentInformation = addCommonProperties(hospitalContent);
            // Make the AJAX request
            makeAjaxRequest({
                url: "/Hospital/InsertOrUpdateHospitalContentInformation",
                data: hospitalContentInformation,
                type: 'POST',
                successCallback: function (response) {
                    self.hospitalContentInformation = response.data;
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