$(document).ready(function () {
    // Initialize DataTables
    initializeDataTable();
    
    // Initialize event handlers
    initializeEventHandlers();

    // Initialize datepicker with consistent format
    $('.datepicker').datepicker({
        format: 'dd/MM/yyyy',
        autoclose: true,
        todayHighlight: true,
        orientation: 'bottom',
        clearBtn: true
    });

    // Set default dates
    var today = new Date();
    $('#FromDate').datepicker('setDate', today);
    $('#ToDate').datepicker('setDate', today);
});

function initializeDataTable() {
    var groupCol = $("input[name='groupby']:checked").val();
    var table = $('#Psummary').DataTable({
        orderCellsTop: true,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                exportOptions: {
                    format: {
                        body: function (data, row, column, node) {
                            return data.replace('£', '').replace('$', '').replace('R', '');
                        }
                    }
                }
            }
        ],
        stripeClasses: ['odd-row', 'even-row'],
        columnDefs: [
            { visible: false, targets: +groupCol }
        ],
        order: [[groupCol, 'asc']],
        displayLength: 100,
        drawCallback: function (settings) {
            handleGrouping(this.api(), groupCol);
        }
    });

    // Add search boxes to header
    $('#Psummary thead tr:eq(1) th').each(function (i) {
        var title = $(this).text();
        $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

        $('input', this).on('keyup change', function () {
            if (table.column(i).search() !== this.value) {
                table.column($(this).parent().index() + ':visible')
                    .search(this.value)
                    .draw();
            }
        });
    });

    // Handle visibility based on show/hide toggles
    updateColumnVisibility();
}

function handleGrouping(api, groupCol) {
    var rows = api.rows({ page: 'current' }).nodes();
    var last = null;
    var aData = {};

    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {
        var vals = api.row(api.row($(rows).eq(i)).index()).data();
        var salary = parseFloat((vals[8] || "0").replace(/[£$R,]/g, ""));

        if (!aData[group]) {
            aData[group] = {
                rows: [],
                salary: []
            };
        }

        aData[group].rows.push(i);
        aData[group].salary.push(salary);
    });

    for (var group in aData) {
        var sum = aData[group].salary.reduce((a, b) => a + b, 0);
        var idx = Math.max(...aData[group].rows);
        var symbol = api.row(aData[group].rows[0]).data()[8]?.charAt(0) || '';

        var colspan = $('#lbtnsize').text() === 'Show Location' && $('#LinksizeA').text() === 'Show Size' ? 6 : 5;

        $(rows).eq(idx).after(
            '<tr class="group">' +
            '<td colspan="7">' + $("input[name='groupby']:checked").attr('title') + ' : ' + group + '</td>' +
            '<td colspan="' + colspan + '">Total : ' + symbol + ' ' + sum.toFixed(2) + '</td>' +
            '</tr>'
        );
    }
}

function initializeEventHandlers() {
    // Duration change handler
    $('#Duration').change(function () {
        var showCustom = $(this).val() === 'Custom';
        $('#customDateRange').toggle(showCustom);
    });

    // Category change handler
    $('#CategoryId').change(function () {
        var categoryId = $(this).val();
        updateProducts(categoryId);
    });

    // Group by radio buttons
    $('input[name="groupby"]').change(function () {
        var table = $('#Psummary').DataTable();
        table.destroy();
        initializeDataTable();
    });

    // Row hover effect
    $("#Psummary").on('mouseenter', 'tr:not(.group)', function () {
        $(this).css({
            "background": "#efefef",
            "cursor": "pointer",
            "font-weight": "bold",
            "box-shadow": "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset",
            "-webkit-box-shadow": "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset"
        });
    }).on('mouseleave', 'tr:not(.group)', function () {
        $(this).css({
            "background": "",
            "cursor": "",
            "font-weight": "",
            "box-shadow": "",
            "-webkit-box-shadow": ""
        });
    });
}

function updateProducts(categoryId) {
    $.get('/PSummaryReport/GetProducts', { categoryId: categoryId })
        .done(function (data) {
            var $select = $('#ProductId');
            $select.empty();
            $select.append($('<option></option>').val('').text('Select Product'));
            $.each(data, function (i, item) {
                $select.append($('<option></option>').val(item.value).text(item.text));
            });
        });
}

function updateColumnVisibility() {
    var table = $('#Psummary').DataTable();
    
    if ($('#lbtnsize').text() === 'Show Location') {
        table.column(13).visible(false);
    }
    
    if ($('#LinksizeA').text() === 'Show Size') {
        table.column(12).visible(false);
    }
}

function toggleLocation() {
    $.post('/PSummaryReport/ToggleLocation')
        .done(function (response) {
            if (response.success) {
                var $btn = $('#lbtnsize');
                var showLocation = $btn.text() === 'Show Location';
                $btn.text(showLocation ? 'Hide Location' : 'Show Location');
                
                var table = $('#Psummary').DataTable();
                table.column(13).visible(showLocation);
                table.draw();
            }
        });
}

function toggleSize() {
    $.post('/PSummaryReport/ToggleSize')
        .done(function (response) {
            if (response.success) {
                var $btn = $('#LinksizeA');
                var showSize = $btn.text() === 'Show Size';
                $btn.text(showSize ? 'Hide Size' : 'Show Size');
                
                var table = $('#Psummary').DataTable();
                table.column(12).visible(showSize);
                table.draw();
            }
        });
} 