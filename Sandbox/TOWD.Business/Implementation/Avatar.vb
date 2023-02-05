Friend Class Avatar
    Implements IAvatar

    Private _worldData As WorldData

    Public Sub New(worldData As WorldData)
        _worldData = worldData
    End Sub

    Public ReadOnly Property MapName As String Implements IAvatar.MapName
        Get
            Return _worldData.Avatar.MapName
        End Get
    End Property

    Public ReadOnly Property MapColumn As Integer Implements IAvatar.MapColumn
        Get
            Return _worldData.Avatar.MapColumn
        End Get
    End Property

    Public ReadOnly Property MapRow As Integer Implements IAvatar.MapRow
        Get
            Return _worldData.Avatar.MapRow
        End Get
    End Property
End Class
