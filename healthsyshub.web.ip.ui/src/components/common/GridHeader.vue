<template>
    <div class="card-header card-header-padding">
        <div class="float-left">
            <span @click="$emit('add')" class="padding-rigt custom-cursor btn btn-success">
                <i class="fa fa-plus-circle" title="add"></i>
            </span>
            <span @click="$emit('edit')" class="padding-rigt custom-cursor btn btn-primary icon-margin">
                <i class="fa fa-pen" title="edit"></i>
            </span>
            <span class="padding-rigt custom-cursor btn btn-danger icon-margin">
                <i class="fa fa-trash" title="delete"></i>
            </span>
            <span class="padding-rigt custom-cursor btn btn-warning icon-margin">
                <i class="fa fa-plus-square" title="copy"></i>
            </span>
        </div>
        <div class="float-right">
            <span class="custom-cursor padding-right btn btn-secondary permission-hidden">
                <i class="fa fa-upload permission-hidden" title="import"></i>
            </span>
            <span class="custom-cursor btn btn-info icon-margin" data-toggle="dropdown">
                <i class="fa fa-download" title="export"></i>
            </span>
            <div class="dropdown-menu dropdown-menu-right">
                <a class="dropdown-item" href="#" id="exportTemplate">Export Template</a>
                <a class="dropdown-item" href="#" id="exportWithGridData">Export with Grid Data</a>
                <a class="dropdown-item" href="#" id="exportWithOriginalData">Export with Original Data</a>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "gridheader",
    setup() {
        return {}
    }
}
</script>

<style scoped>
.icon-margin {
    margin-left: 3px !important;
}

.padding-rigt {
    padding-right: 20px !important;
}

.custom-cursor {
    cursor: pointer !important;
}
</style>