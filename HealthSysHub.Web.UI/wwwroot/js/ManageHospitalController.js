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

            self.HospitalInformation = responses[0]?.data || {};

            self.dbHospitalTypes = responses[1]?.data || [];

            self.dbDepartments = responses[2]?.data || [];

            self.dbSpecializations = responses[3]?.data || [];

            genarateDropdown("HospitalTypeId", self.dbHospitalTypes, "HospitalTypeId", "HospitalTypeName");

            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
        });
    };
}