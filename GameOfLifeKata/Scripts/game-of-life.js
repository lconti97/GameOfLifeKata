const AttrRowIndex = 1;
const AttrColumnIndex = 2;

var cells = new Array(10);
for (var i = 0; i < cells.length; i++) {
    cells[i] = new Array(10);
    for (var j = 0; j < 10; j++) {
        cells[i][j] = -1;
    }
}

$("#do-stuff").on("click", function (event) {
    $.ajax({
        url: 'http://' + window.location.host + '/api/GameOfLife/PostInitialGeneration',
        data: {
            '': cells
        },
        method: "POST",
        success: function (result) {
            $.ajax({
                url: 'http://' + window.location.host + '/api/GameOfLife/GetNextGeneration',
                method: "GET",
                success: function (result) {
                    for (var row = 0; row < result.length; row++)
                        for (var column = 0; column < result[row].length; column++) {
                            var oldLifeStateCode = cells[row][column];
                            var oldClass = getLifeStateClass(oldLifeStateCode);
                            var newLifeStateCode = result[row][column];
                            var newClass = getLifeStateClass(newLifeStateCode);

                            var cell = $("#cell-" + row + "-" + column);
                            replaceClass(cell, oldClass, newClass);
                            cells[row][column] = newLifeStateCode;
                        }
                }
            });
        }
    });
});

$(".cell").on("click", function (event) {
    var attrs = this.id.split('-');
    var row = attrs[AttrRowIndex];
    var column = attrs[AttrColumnIndex];

    var oldLifeStateCode = cells[row][column];
    var oldClass = getLifeStateClass(oldLifeStateCode);
    var newLifeStateCode = getNextLifeStateCode(cells[row][column]); 
    var newClass = getLifeStateClass(newLifeStateCode);

    replaceClass($(this), oldClass, newClass);
    cells[row][column] = newLifeStateCode;
});

var replaceClass = function (cell, oldClass, newClass) {
    cell.removeClass(oldClass);
    cell.addClass(newClass);
}

var getLifeStateClass = function (lifeStateCode) {
    if (lifeStateCode === 0)
        return "alive-cell";
    else if (lifeStateCode === -1)
        return "never-alive-cell";
    else
        return "dead-cell";
}

var getNextLifeStateCode = function (currentLifeStateCode) {
    if (currentLifeStateCode === 0)
        return 1;
    else
        return 0;
}