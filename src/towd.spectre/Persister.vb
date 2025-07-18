Imports System.IO
Imports System.Text.Json
Imports towd.data
Imports towd.ui

Friend Class Persister
    Implements IPersister

    Public Function SaveGameAsync(saveSlot As ISaveSlot, worldData As WorldData) As Task Implements IPersister.SaveGameAsync
        Return Task.Run(Sub()
                            File.WriteAllText(saveSlot.Filename, JsonSerializer.Serialize(worldData))
                        End Sub)
    End Function

    Public Function SaveExists(saveSlot As ISaveSlot) As DateTime? Implements IPersister.SaveExists
        If File.Exists(saveSlot.Filename) Then
            Return File.GetLastWriteTime(saveSlot.Filename)
        Else
            Return Nothing
        End If
    End Function

    Public Function LoadGame(saveSlot As ISaveSlot) As WorldData Implements IPersister.LoadGame
        Try
            Return JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText(saveSlot.Filename))
        Catch
            Return Nothing
        End Try
    End Function
End Class
