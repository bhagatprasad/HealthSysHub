function HospitalController() {
    var self = this;

    self.dbHospitalTypes = [];

    self.ApplicationUser = {};

    self.selectedRows = [];

    self.currentSelectedHospital = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedHospitals = [];

    var actions = [];

    var dataObjects = [];

    actions.push('/HospitalType/FetchHospitalTypes');

    var appuser = storageService.get("ApplicationUser");

    if (appuser) {
        self.ApplicationUser = appuser;
    }
    self.init = function () {

        $("#manageHospitalBtn").removeClass("permission-hidden");

        showLoader();

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            return $.ajax(ajaxConfig);
        }); $.when.apply($, requests).done(function (...responses) {
            self.dbHospitalTypes = responses[0]?.data || [];
            genarateDropdown("HospitalTypeId", self.dbHospitalTypes, "HospitalTypeId", "HospitalTypeName");
            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        var table = new Tabulator("#HospitalGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/Hospital/FetchHospitals',
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
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentHospitalChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().HospitalId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childHospitalChkbox-${rowId}' class='childHospitalChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                {
                    title: "Logo",
                    field: "LogoUrl",
                    formatter: function (cell) {
                        return `<img src="${cell.getValue()}" alt="Logo" style="width:50px;height:50px;" />`;
                    },
                    align: "center"
                },
                { title: "Hospital Name", field: "HospitalName" },
                { title: "Hospital Code", field: "HospitalCode" },
                { title: "Registration Number", field: "RegistrationNumber" },
                { title: "Address", field: "Address" },
                { title: "City", field: "City" },
                { title: "State", field: "State" },
                { title: "Country", field: "Country" },
                { title: "Postal Code", field: "PostalCode" },
                { title: "Phone Number", field: "PhoneNumber" },
                { title: "Email", field: "Email" },
                { title: "Website", field: "Website" },
                { title: "Created By", field: "CreatedBy" },
                { title: "Created On", field: "CreatedOn" },
                { title: "Modified By", field: "ModifiedBy" },
                { title: "Modified On", field: "ModifiedOn" },
                {
                    title: "Is Active",
                    field: "IsActive",
                    formatter: "tickCross",
                    align: "center"
                }
            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentHospitalChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().HospitalId === changedRow.HospitalId);

                    if (foundRow) {
                        var rowId = foundRow.getData().HospitalId;
                        var checkbox = document.querySelector(`#childHospitalChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currentSelectedHospital = changedRow;
                        }
                        else {
                            self.currentSelectedHospital = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentHospitalChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childHospitalChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childHospitalChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.HospitalId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentHospitalChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currentSelectedHospital) {
                // Map all properties to the form fields
                $("#HospitalName").val(self.currentSelectedHospital.HospitalName);
                $("#HospitalCode").val(self.currentSelectedHospital.HospitalCode);
                $("#RegistrationNumber").val(self.currentSelectedHospital.RegistrationNumber);
                $("#Address").val(self.currentSelectedHospital.Address);
                $("#City").val(self.currentSelectedHospital.City);
                $("#State").val(self.currentSelectedHospital.State);
                $("#Country").val(self.currentSelectedHospital.Country);
                $("#PostalCode").val(self.currentSelectedHospital.PostalCode);
                $("#PhoneNumber").val(self.currentSelectedHospital.PhoneNumber);
                $("#Email").val(self.currentSelectedHospital.Email);
                $("#Website").val(self.currentSelectedHospital.Website);
                $("#LogoUrl").val(self.currentSelectedHospital.LogoUrl);
                $("#HospitalTypeId").val(self.currentSelectedHospital.HospitalTypeId);
                // Show the sidebar and add backdrop
                $('#sidebar').addClass('show');
                $('body').append('<div class="modal-backdrop fade show"></div>');
            } else {
                // Hide the sidebar and remove backdrop if no hospital is selected
                $('#sidebar').removeClass('show');
                $('.modal-backdrop').remove();
            }
        });

        $('#addBtn').on('click', function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $('#closeSidebar, .modal-backdrop').on('click', function () {
            $('#AddEditHospitalForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $('#manageHospitalBtn').on('click', function () {
            if (self.currentSelectedHospital) {
                window.location.href = "/Hospital/ManageHospital?hospitalId=" + self.currentSelectedHospital.HospitalId;
            }
        });

        $('#AddEditHospitalForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditHospitalForm');
            var hospital = addCommonProperties(formData);
            hospital.HospitalId = self.currentSelectedHospital ? self.currentSelectedHospital.HospitalId : null;

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
                        if (!iscopy) {
                            $('#AddEditHospitalForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currentSelectedHospital = {};
                    }
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });
        };

    };
}