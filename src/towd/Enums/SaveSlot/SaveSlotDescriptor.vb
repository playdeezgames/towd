Imports System.IO
Imports System.Text.Json
Imports towd.business

Public Class SaveSlotDescriptor
    Implements ISaveSlot
    Sub New(
           saveSlot As SaveSlot,
           displayName As String,
           filename As String)
        Me.SaveSlot = saveSlot
        Me.DisplayName = displayName
        Me.Filename = filename
    End Sub

    Public ReadOnly Property SaveSlot As SaveSlot Implements ISaveSlot.SaveSlot

    Public ReadOnly Property DisplayName As String Implements ISaveSlot.DisplayName

    Public ReadOnly Property Filename As String Implements ISaveSlot.Filename

    Public Sub SaveGame(world As IWorld) Implements ISaveSlot.SaveGame
        File.WriteAllText(Filename, JsonSerializer.Serialize(world.Data))
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = DisplayName
        If File.Exists(Filename) Then

            result &= $"(Last Saved {File.GetLastWriteTime(Filename)})"
        End If
        Return result
    End Function
End Class
