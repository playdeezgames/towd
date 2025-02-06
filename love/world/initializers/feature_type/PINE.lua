local feature_type = require "world.feature_type"
local feature = require "world.feature"
local statistic_type = require "world.statistic_type"
feature_type.set_initializer(
    feature_type.PINE, 
    function(feature_id) 
        feature.set_statistic(feature_id, statistic_type.HIT_POINTS, 10)
    end)
feature_type.set_describer(
    feature_type.PINE, 
    function(feature_id) 
        local hit_points = feature.get_statistic(feature_id, statistic_type.HIT_POINTS)
        return "This is a tree("..hit_points.." hp). You punch it!"
    end)

return nil