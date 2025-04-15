function ManageConsultationAppointmentController() {

    var self = this;

    var actions = [];

    var dataObjects = [];

    self.dbLabTests = [];

    self.dbMedicines = [];

    self.ConsultationDetails = {};

    self.appointmentId;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');

        if (appUserInfo)
            self.ApplicationUser = appUserInfo;

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

            self.ConsultationDetails = responses[1][0].data ? responses[1][0].data : {};

            console.log(self.ConsultationDetails);

            hideLoader();

        }).fail(function () {
            console.log('One or more requests failed.');
            hideLoader();
        });

    };

};