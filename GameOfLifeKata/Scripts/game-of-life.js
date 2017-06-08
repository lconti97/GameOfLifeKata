const AttrRowIndex = 1;
const AttrColumnIndex = 2;
const GridRows = 10;
const GridColumns = 10;
const NeverAliveCode = -1;
const AliveCode = 0;
const NewlyDeadCode = 1;

var encodedCellGrid;

var getNewEncodedCellsGrid = function () {
    var cells = new Array(GridRows);
    for (var row = 0; row < GridRows; row++) {
        cells[row] = new Array(GridColumns);
        for (var columns = 0; columns < GridColumns; columns++) {
            cells[row][columns] = NeverAliveCode;
        }
    }
    return cells;
}
var encodedCellGrid = getNewEncodedCellsGrid();

var postInitialGeneration = function (onSuccess) {
    $.ajax({
        url: 'http://' + window.location.host + '/api/GameOfLife/PostInitialGeneration',
        data: {
            '': encodedCellGrid
        },
        method: "POST",
        success: onSuccess
    });
}

var getNextGeneration = function (onSuccess) {
    $.ajax({
        url: 'http://' + window.location.host + '/api/GameOfLife/GetNextGeneration',
        method: "GET",
        success: updateGrid
    });
}

var updateGrid = function (newEncodedCellGrid) {
    var oldEncodedCellGrid = encodedCellGrid;

    for (var row = 0; row < GridRows; row++) {
        for (var column = 0; column < GridColumns; column++) {
            var cell = getCellJQuery(row, column);
            cell.removeClass(getLifeStateClass(oldEncodedCellGrid[row][column]));
            cell.addClass(getLifeStateClass(newEncodedCellGrid[row][column]));
        }
    }

    encodedCellGrid = newEncodedCellGrid;
}

var getCellJQuery = function(row, column) {
    return $("#cell-" + row + "-" + column);
}

var onStepButtonClick = function (event) {
    postInitialGeneration(getNextGeneration);
    
};

$("#do-stuff").on("click", onStepButtonClick);

$(".cell").on("click", function (event) {
    var attrs = this.id.split('-');
    var row = attrs[AttrRowIndex];
    var column = attrs[AttrColumnIndex];

    var oldLifeStateCode = encodedCellGrid[row][column];
    var oldClass = getLifeStateClass(oldLifeStateCode);
    var newLifeStateCode = getNextLifeStateCode(encodedCellGrid[row][column]); 
    var newClass = getLifeStateClass(newLifeStateCode);

    replaceClass($(this), oldClass, newClass);
    encodedCellGrid[row][column] = newLifeStateCode;

});

var replaceClass = function (cell, oldClass, newClass) {
    cell.removeClass(oldClass);
    cell.addClass(newClass);
}

var getLifeStateClass = function (lifeStateCode) {
    if (lifeStateCode === AliveCode)
        return "alive-cell";
    else if (lifeStateCode === NeverAliveCode)
        return "never-alive-cell";
    else
        return "dead-cell";
}

var getNextLifeStateCode = function (currentLifeStateCode) {
    if (currentLifeStateCode === AliveCode)
        return NewlyDeadCode;
    else
        return AliveCode;
}