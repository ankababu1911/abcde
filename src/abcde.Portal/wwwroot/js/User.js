function userEvents() {
    $('#userTable').DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": false,
        "info": true,
        "autoWidth": false,
        responsive: true,
        rowReorder: {
            selector: 'td:nth-child(2)'
        }
    });
    //function PaginationButtons() {
    //    let recordCount = dataTable.page.info().recordsDisplay;
    //    let showEntries = parseInt($('.dataTables_length select').val());
    //    if (recordCount <= showEntries) {
    //        $('.paginate_button').hide();
    //    } else {
    //        $('.paginate_button').show();
    //    }
    //}
    //$('.dataTables_length select').on('change', function () {
    //    PaginationButtons();
    //});
    //PaginationButtons();
}
function multiselect() {
    $('.multiselect').select2({
        theme: "bootstrap-5",
        dropdownParent: $('.modal'),
        width: '100%',
        closeOnSelect: false,
        placeholder: 'Student',
        
    });
  
}