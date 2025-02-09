local picker_state = require "game.picker_state"
local states       = require "game.states"
local hues         = require "gfx.hues"
local M = {}

function M.new(parent)
    local EMBARK_TEXT = "Embark!"
    local LOAD_TEXT = "Load..."
    local OPTIONS_TEXT = "Options..."
    local ABOUT_TEXT = "About..."
    local QUIT_TEXT = "Quit"
    local MAIN_MENU_TEXT = "Main Menu:"
    local instance = picker_state.new(parent, MAIN_MENU_TEXT, hues.WHITE, {
        EMBARK_TEXT,
        LOAD_TEXT,
        OPTIONS_TEXT,
        ABOUT_TEXT,
        QUIT_TEXT
    })
    function instance:on_menu_item(menu_item)
        if menu_item == QUIT_TEXT then
            self:get_parent():set_state(states.CONFIRM_QUIT)
        end
    end
    function instance:on_cancel()
        self:get_parent():set_state(states.CONFIRM_QUIT)
    end
    return instance
end

return M