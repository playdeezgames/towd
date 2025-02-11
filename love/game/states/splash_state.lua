local state = require "scene.state"
local states = require "game.states.states"
local commands = require "game.commands"
local fonts = require "game.gfx.fonts"
local hues  = require "game.gfx.hues"
local gfx   = require "gfx.gfx"
local images= require "game.gfx.images"
local label = require "ui.label"
local decal = require "ui.decal"
local shadow_label = require "ui.shadow_label"
local sources      = require "game.sources"
local source_id    = require "game.source_id"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    function instance:on_command(command)
        if command == commands.GREEN then
            self:get_parent():set_state(states.MAIN_MENU)
            sources[source_id.BOOP]:play()
        end
    end
    function instance:on_load()
        decal.new(self, images.SPLASH, 0, 0)
        local view_size = gfx.get_size()
        shadow_label.new(self, fonts.M6X11PLUS_96, "Tomb of Woeful DOOM!", view_size.width/2,view_size.height / 2 - fonts.M6X11PLUS_96:get_height()/2,4,4, hues.LIGHT_CYAN, hues.CYAN, label.CENTER)
        shadow_label.new(self, fonts.M6X11PLUS_48, "Press SPACE or (A)", view_size.width/2,view_size.height - fonts.M6X11PLUS_48:get_height(),2,2, hues.WHITE, hues.DARK_GRAY, label.CENTER)
    end
    return instance
end

return M