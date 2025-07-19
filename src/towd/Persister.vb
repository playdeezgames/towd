Imports System.IO
Imports System.Text.Json
Imports towd.data
Imports towd.ui

Friend Class Persister
    Implements IPersister

    Public Function SaveGame(SaveSlot As ISaveSlot, worldData As WorldData) As Task Implements IPersister.SaveGame
        File.WriteAllText(SaveSlot.Filename, JsonSerializer.Serialize(worldData))
        Return Task.CompletedTask
    End Function

    Public Function SaveExists(saveSlot As ISaveSlot) As Task(Of DateTime?) Implements IPersister.SaveExists
        If File.Exists(saveSlot.Filename) Then
            Return Task.FromResult(Of Date?)(File.GetLastWriteTime(saveSlot.Filename))
        Else
            Return Task.FromResult(Of Date?)(Nothing)
        End If
    End Function

    Public Function LoadGame(saveSlot As ISaveSlot) As Task(Of WorldData) Implements IPersister.LoadGame
        Try
            Return Task.FromResult(JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText(saveSlot.Filename)))
        Catch
            Return Task.FromResult(Of WorldData)(Nothing)
        End Try
    End Function
End Class
