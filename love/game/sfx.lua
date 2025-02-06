local M = {}

local sources = {}

function M.load()
end

function M.play(sfx_id)
    love.audio.play(sources[sfx_id])
end

return M