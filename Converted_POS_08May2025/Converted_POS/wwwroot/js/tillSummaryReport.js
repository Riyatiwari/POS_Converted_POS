$(document).ready(function () {
    var groupCol = 0;
    var groupTitle = "";

    // Initialize DataTables
    var table = $('#tillSummaryTable').DataTable({
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
            { visible: false, targets: groupCol }
        ],
        order: [[groupCol, 'asc']],
        displayLength: 100,
        searching: true,
        drawCallback: function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;
            var subTotal = [];
            var groupID = -1;
            var aData = [];
            var index = 0;
            var symbol = "";

            // Process data for grouping
            api.column(groupCol, { page: 'current' }).data().each(function (group, i) {
                var vals = api.row(api.row($(rows).eq(i)).index()).data();

                var salary1 = vals[5] || 0;
                var gross1 = vals[6] || 0;
                var dis1 = vals[7] || 0;
                var net1 = vals[8] || 0;
                var Surcharge1 = vals[10] || 0;
                var VAT1 = vals[11] || 0;
                var VAT0 = vals[12] || 0;
                var VAT5 = vals[13] || 0;
                var VAT12 = vals[14] || 0;
                var VAT20 = vals[15] || 0;
                var dept11 = vals[16] || 0;
                var dept21 = vals[17] || 0;
                var dept31 = vals[18] || 0;
                var dept41 = vals[19] || 0;

                // Parse values
                var salary = parseFloat(salary1.toString().replace(/[£$R,]/g, ""));
                var gross = parseFloat(gross1.toString().replace(/[£$R,]/g, ""));
                var dis = parseFloat(dis1.toString().replace(/[£$R,]/g, ""));
                var net = parseFloat(net1.toString().replace(/[£$R,]/g, ""));
                var vat = parseFloat(VAT1.toString().replace(/[£$R,]/g, ""));
                var vat0 = parseFloat(VAT0.toString().replace(/[£$R,]/g, ""));
                var vat5 = parseFloat(VAT5.toString().replace(/[£$R,]/g, ""));
                var vat12 = parseFloat(VAT12.toString().replace(/[£$R,]/g, ""));
                var vat20 = parseFloat(VAT20.toString().replace(/[£$R,]/g, ""));
                var Surcharge = parseFloat(Surcharge1.toString().replace(/[£$R,]/g, ""));
                var dept1 = parseFloat(dept11.toString().replace(/[£$R,]/g, ""));
                var dept2 = parseFloat(dept21.toString().replace(/[£$R,]/g, ""));
                var dept3 = parseFloat(dept31.toString().replace(/[£$R,]/g, ""));
                var dept4 = parseFloat(dept41.toString().replace(/[£$R,]/g, ""));

                if (typeof aData[group] == 'undefined') {
                    aData[group] = {
                        rows: [],
                        salary: [],
                        gross: [],
                        dis: [],
                        net: [],
                        Surcharge: [],
                        vat: [],
                        vat0: [],
                        vat5: [],
                        vat12: [],
                        vat20: [],
                        dept1: [],
                        dept2: [],
                        dept3: [],
                        dept4: []
                    };
                }

                aData[group].rows.push(i);
                aData[group].salary.push(salary);
                aData[group].gross.push(gross);
                aData[group].dis.push(dis);
                aData[group].net.push(net);
                aData[group].Surcharge.push(Surcharge);
                aData[group].vat.push(vat);
                aData[group].vat0.push(vat0);
                aData[group].vat5.push(vat5);
                aData[group].vat12.push(vat12);
                aData[group].vat20.push(vat20);
                aData[group].dept1.push(dept1);
                aData[group].dept2.push(dept2);
                aData[group].dept3.push(dept3);
                aData[group].dept4.push(dept4);
            });

            // Add group headers and totals
            var idx = 0;
            var idx1 = 0;
            var last = null;
            var p_val = 0;

            // Add grand total
            var grandTotal = $('#hd_NetTotal').val();
            $(rows).eq(0).before(
                '<tr class="group"><td colspan="7"> Grand Total </td>' +
                '<td colspan="12"> ' + symbol + ' ' + parseFloat(grandTotal).toFixed(2) + ' </td></tr>'
            );

            // Add group totals
            for (var Venue in aData) {
                idx = Math.max.apply(Math, aData[Venue].rows);

                var sum = 0.0;
                var grosssum = 0.0;
                var dissum = 0.0;
                var netsum = 0.0;
                var SurchargeSum = 0.0;
                var vatSum = 0.0;
                var vatSum0 = 0.0;
                var vatSum5 = 0.0;
                var vatSum12 = 0.0;
                var vatSum20 = 0.0;
                var dept1sum = 0.0;
                var dept2sum = 0.0;
                var dept3sum = 0.0;
                var dept4sum = 0.0;

                // Calculate sums
                aData[Venue].gross.forEach(v => grosssum += v);
                aData[Venue].salary.forEach(v => sum += v);
                aData[Venue].Surcharge.forEach(v => SurchargeSum += v);
                aData[Venue].dis.forEach(v => dissum += v);
                aData[Venue].net.forEach(v => netsum += v);
                aData[Venue].vat.forEach(v => vatSum += v);
                aData[Venue].vat0.forEach(v => vatSum0 += v);
                aData[Venue].vat5.forEach(v => vatSum5 += v);
                aData[Venue].vat12.forEach(v => vatSum12 += v);
                aData[Venue].vat20.forEach(v => vatSum20 += v);
                aData[Venue].dept1.forEach(v => dept1sum += v);
                aData[Venue].dept2.forEach(v => dept2sum += v);
                aData[Venue].dept3.forEach(v => dept3sum += v);
                aData[Venue].dept4.forEach(v => dept4sum += v);

                // Add group row
                var groupRow = 
                    '<tr class="group">' +
                    '<td colspan="4">' + groupTitle + ' : ' + Venue + '</td>' +
                    '<td colspan="1">' + parseFloat(sum).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(grosssum).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(dissum).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(netsum).toFixed(2) + '</td>' +
                    '<td colspan="1"></td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(SurchargeSum).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(vatSum).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(vatSum0).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(vatSum5).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(vatSum12).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(vatSum20).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(dept1sum).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(dept2sum).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(dept3sum).toFixed(2) + '</td>' +
                    '<td colspan="1">' + symbol + ' ' + parseFloat(dept4sum).toFixed(2) + '</td>' +
                    '</tr>';

                if (idx1 === 0) {
                    if (p_val === 1) {
                        $(rows).eq(idx1).after(groupRow);
                    } else {
                        $(rows).eq(idx1).before(groupRow);
                        p_val = idx1 + 1;
                    }
                } else {
                    $(rows).eq(idx1).after(groupRow);
                }

                idx1 = idx;
            }
        }
    });

    // Add search functionality to each column
    $('#tillSummaryTable thead tr:eq(1) th').each(function (i) {
        var title = $(this).text();
        $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

        $('input', this).on('keyup change', function () {
            if (table.column(i).search() !== this.value) {
                table.column($(this).parent().index() + ':visible')
                    .search(this.value)
                    .draw();
            } else {
                table.column($(this).parent().index() + ':visible')
                    .search('')
                    .draw();
            }
        });
    });

    // Handle duration change
    $('#Duration').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue === 'Custom') {
            $('#customDateRange').show();
        } else {
            $('#customDateRange').hide();
        }
    });

    // Handle row hover effects
    $("#tillSummaryTable tr").not(':first').hover(
        function () {
            $(this).css({
                "background": "#efefef",
                "cursor": "Pointer",
                "font-weight": "bold",
                "box-shadow": "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset",
                "-webkit-box-shadow": "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset"
            });
        },
        function () {
            $(this).css({
                "background": "",
                "cursor": "",
                "font-weight": "",
                "box-shadow": "",
                "-webkit-box-shadow": ""
            });
        }
    );

    // Set initial group column and title based on radio selection
    function updateGrouping() {
        if ($('#chkPs2').is(':checked')) {
            groupCol = 0;
            groupTitle = 'Venue';
        } else if ($('#chkPG1').is(':checked')) {
            groupCol = 0;
            groupTitle = 'Till Total';
        } else if ($('#chkPs4').is(':checked')) {
            groupCol = 1;
            groupTitle = 'Location';
        }
        table.draw();
    }

    // Handle radio button changes
    $('input[name="groupby"]').change(function() {
        updateGrouping();
    });

    // Initial setup
    updateGrouping();
}); 