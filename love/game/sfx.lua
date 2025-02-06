local M = {}

M.ENTER_PORTAL = "ENTER_PORTAL"

local sources = {}

function M.load()
    sources[M.ENTER_PORTAL] = love.audio.newSource("assets/sfx/ENTER_PORTAL.wav","static")
end

function M.play(sfx_id)
    love.audio.play(sources[sfx_id])
end

return M