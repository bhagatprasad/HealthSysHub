function StaffController() {

    var self = this;

    self.selectedRows = [];

    self.currectSelectedStaff = {};

    self.todayDate = new Date();

    self.fileUploadModal = $("#fileUploadModal");

    self.ImportedDStaffs = [];

    self.ApplicationUser = {};

    self.dbSpecializations = [];

    self.dbDepartments = [];

    self.dbRoles = [];

    var actions = [];

    var dataObjects = [];

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');

        if (appUserInfo)
            self.ApplicationUser = appUserInfo;

        actions.push('/Role/FetchUserRoles');

        actions.push('/Department/FetchDepartments');

        actions.push('/Specialization/FetchSpecializations');

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 0) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });
        $.when.apply($, requests).done(function () {
            var responses = arguments;

            console.log(responses);

            self.dbRoles = responses[0][0].data ? responses[0][0].data : [];

            self.dbDepartments = responses[2][0].data ? responses[1][0].data : [];

            self.dbSpecializations = responses[2][0].data ? responses[2][0].data :[];

            genarateDropdown("RoleId", self.dbRoles, "RoleId", "Name");

            genarateDropdown("DepartmentId", self.dbDepartments, "DepartmentId", "DepartmentName");

            genarateDropdown("SpecializationId", self.dbSpecializations, "SpecializationId", "SpecializationName");
           
            hideLoader();

        }).fail(function () {
            console.log('One or more requests failed.');
            hideLoader();
        });

        var table = new Tabulator("#StaffGrid", {
            height: "770px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/Staff/FetchAllHospitalStaff',
            ajaxParams: { hospitalId: self.ApplicationUser.HospitalId },
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            },
            ajaxResponse: function (url, params, response) {
                return response.data;
            },
            columns: getStaffColumnConfig(window.innerWidth <= 768),
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentStaffChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().StaffId === changedRow.StaffId);

                    if (foundRow) {
                        var rowId = foundRow.getData().StaffId;
                        var checkbox = document.querySelector(`#childStaffChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currentSelectedStaff = changedRow;
                        }
                        else {
                            self.currentSelectedStaff = {};
                        }
                    }
                }
            }
        });

        // Define column configurations for staff
        function getStaffColumnConfig(isMobile) {
            const baseColumns = [
                // Checkbox column (always visible)
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentStaffChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().StaffId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childStaffChkbox-${rowId}' class='childStaffChkbox' data-row-id='${rowId}'/></div>`;
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
                    title: "Staff Details",
                    field: "FirstName",
                    formatter: function (cell, formatterParams) {
                        const data = cell.getData();
                        return `
                    <div class="mobile-staff-view">
                        <div class="staff-mobile-row">
                            <strong>${data.FirstName || ''} ${data.LastName || ''}</strong>
                        </div>
                        <div class="staff-mobile-row">
                            <i class="fas fa-envelope"></i> ${data.Email || 'N/A'}
                        </div>
                        <div class="staff-mobile-row">
                            <i class="fas fa-phone"></i> ${data.Phone || 'N/A'}
                        </div>
                        <div class="staff-mobile-row">
                            <i class="fas fa-briefcase"></i> ${data.Designation || 'N/A'}
                        </div>
                        <div class="staff-mobile-row">
                            <span class="status-badge">${data.IsActive ? 'Active' : 'Inactive'}</span>
                        </div>
                    </div>
                `;
                    }
                });
            } else {
                // Desktop configuration - full columns
                baseColumns.push(
                    { title: "First Name", field: "FirstName" },
                    { title: "Last Name", field: "LastName" },
                    { title: "Email", field: "Email" },
                    { title: "Phone", field: "Phone" },
                    { title: "Designation", field: "Designation" },
                    { title: "License Number", field: "LicenseNumber" },
                    { title: "Created On", field: "CreatedOn", formatter: datetimeFormatter },
                    { title: "Modified On", field: "ModifiedOn", formatter: datetimeFormatter },
                    {
                        title: "Is Active",
                        field: "IsActive",
                        formatter: "tickCross",
                        align: "center"
                    }
                );
            }

            return baseColumns;
        }

        // Example formatters (you'll need to implement these)
        function datetimeFormatter(cell) {
            const value = cell.getValue();
            if (!value) return '';
            return new Date(value).toLocaleString();
        }

        // Add responsive behavior
        window.addEventListener('resize', function () {
            table.setColumns(getStaffColumnConfig(window.innerWidth <= 768));
        });

        $(document).on("change", "#parentStaffChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childStaffChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childStaffChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.StaffId === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentStaffChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//

        $(document).on("click", "#editBtn", function () {
            if (self.currentSelectedStaff) {
                // Basic Information
                $("#FirstName").val(self.currentSelectedStaff.FirstName || '');
                $("#LastName").val(self.currentSelectedStaff.LastName || '');
                $("#Email").val(self.currentSelectedStaff.Email || '');
                $("#Phone").val(self.currentSelectedStaff.Phone || '');

                // Professional Information
                $("#Designation").val(self.currentSelectedStaff.Designation || '');

                // GUID properties - need special handling for null/empty GUIDs
                if (self.currentSelectedStaff.RoleId) {
                    $("#RoleId").val(self.currentSelectedStaff.RoleId);
                } else {
                    $("#RoleId").val(''); // or your default option value
                }

                if (self.currentSelectedStaff.DepartmentId) {
                    $("#DepartmentId").val(self.currentSelectedStaff.DepartmentId);
                } else {
                    $("#DepartmentId").val(''); // or your default option value
                }

                if (self.currentSelectedStaff.SpecializationId) {
                    $("#SpecializationId").val(self.currentSelectedStaff.SpecializationId);
                } else {
                    $("#SpecializationId").val(''); // or your default option value
                }

                $("#LicenseNumber").val(self.currentSelectedStaff.LicenseNumber || '');

                // Show the edit form
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
            $('#AddEditStaffForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditStaffForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditStaffForm');
            var staff = addCommonProperties(formData);
            staff.StaffId = self.currectSelectedStaff ? self.currectSelectedStaff.StaffId : null;
            staff.HospitalId = self.ApplicationUser.HospitalId;
            self.addeditStaff(staff);
        });

        makeFormGeneric('#AddEditStaffForm', '#btnsubmit');
        self.addeditStaff = function (staff) {
            makeAjaxRequest({
                url: "/Staff/InsertOrUpdateHospitalStaff",
                data: staff,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        $('#AddEditStaffForm')[0].reset();
                        $('#sidebar').removeClass('show');
                        $('.modal-backdrop').remove();
                        table.setData();
                        self.currectSelectedStaff = {};
                    }
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });
        };
        //---------------Import Functionality-------------//
        $(document).on("click", "#importBtn", function () {
            self.selectedRows = [];
            $(self.fileUploadModal).modal('show');
        });
        $(document).on('change', '#fileInput', function (e) {
            var files = e.target.files;
            processFiles(files, gridColumns.RoleGrid, function (importedData) {
                self.ImportedDepartments = importedData;
                console.log(self.ImportedDepartments);
            });
        });

        $(document).on("click", "#uploadButton", function (e) {
            if (self.ImportedTenants.length > 0) {
                makeAjaxRequest({
                    url: API_URLS.BulkInsertOrUpdateTenant,
                    data: self.ImportedTenants,
                    type: 'POST',
                    successCallback: function (response) {
                        self.ImportedDepartments = [];
                        table.setData();
                        $(self.fileUploadModal).modal('hide');
                        console.info(response);
                    },
                    errorCallback: function (xhr, status, error) {
                        console.error("Error in upserting data to server: " + error);
                    }
                });
            }
        });

        //-------------------Export ------------------//
        $(document).on("click", "#exportTemplate", function (e) {
            var _selectedRows = [];

            self.exportExcel(_selectedRows)
        });
        $(document).on("click", "#exportWithGridData", function (e) {
            if (self.selectedRows.length > 0)
                self.exportExcel(self.selectedRows);
        });
        $(document).on("click", "#exportWithOriginalData", function (e) {
            var gridData = table.getData();
            self.exportExcel(gridData);
        });
        self.exportExcel = function (data) {
            var sorters = table.getSorters();
            var sortColumns = sorters.length > 0 ? sorters[0].field : null;
            var sortOrder = sorters.length > 0 ? sorters[0].dir : null;
            exportToExcel(data, gridColumns.RoleGrid, "Role", "Role_Report", sortColumns, sortOrder);
        }
    };
}