local M = {}
M.NORTH = "NORTH"
M.EAST = "EAST"
M.SOUTH = "SOUTH"
M.WEST = "WEST"
local data = {
    [M.NORTH] = {
        delta_x = 0,
        delta_y = -1
    },
    [M.EAST] = {
        delta_x = 1,
        delta_y = 0
    },
    [M.SOUTH] = {
        delta_x = 0,
        delta_y = 1
    },
    [M.WEST] = {
        delta_x = -1,
        delta_y = 0
    }
}
function M.get_next_position(direction_id, column, row)
    assert(type(direction_id)=="string", "direction_id should be a string")
    assert(type(column)=="number", "column should be a number")
    assert(type(row)=="number", "row should be a number")
    local direction_data = data[direction_id]
    return direction_data.delta_x + column, direction_data.delta_y + row
end
return M