Public Class WorldData
    Public Property AvatarId As Integer?
    Public Property Characters As New List(Of CharacterData)
    Public Property Locations As New List(Of LocationData)
    Public Property Maps As New List(Of MapData)
    Public Property Messages As New List(Of List(Of String))
    Public Property Items As New List(Of ItemData)
End Class
