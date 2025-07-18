﻿Imports System.IO
Imports System.Text.Json
Imports towd.data
Imports towd.ui

Friend Class Persister
    Implements IPersister

    Public Sub SaveGame(saveSlot As ISaveSlot, worldData As WorldData) Implements IPersister.SaveGame
        File.WriteAllText(saveSlot.Filename, JsonSerializer.Serialize(worldData))
    End Sub

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
