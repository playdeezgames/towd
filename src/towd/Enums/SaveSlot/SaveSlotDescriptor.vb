Imports System.IO
Imports System.Text.Json
Imports towd.business
Imports towd.data

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

    Public ReadOnly Property SaveExists As Boolean Implements ISaveSlot.SaveExists
        Get
            Return File.Exists(Filename)
        End Get
    End Property

    Public Sub SaveGame(worldData As WorldData) Implements ISaveSlot.SaveGame
        File.WriteAllText(Filename, JsonSerializer.Serialize(worldData))
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = DisplayName
        If File.Exists(Filename) Then
            result &= $"(Last Saved {File.GetLastWriteTime(Filename)})"
        End If
        Return result
    End Function

    Public Function LoadGame() As WorldData Implements ISaveSlot.LoadGame
        Try
            Return JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText(Filename))
        Catch
            Return Nothing
        End Try
    End Function
End Class
