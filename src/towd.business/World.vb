Imports towd.data

Public Class World
    Implements IWorld
    Private ReadOnly worldData As WorldData

    Sub New(worldData As WorldData)
        Me.worldData = worldData
    End Sub

    Public Sub Initialize() Implements IWorld.Initialize
    End Sub

    Public Sub Abandon() Implements IWorld.Abandon
    End Sub
End Class
