Public Class SaveSlotDescriptor
    Implements ISaveSlot
    Sub New(
           saveSlot As String,
           displayName As String,
           filename As String)
        Me.SaveSlot = saveSlot
        Me.DisplayName = displayName
        Me.Filename = filename
    End Sub

    Public ReadOnly Property SaveSlot As String Implements ISaveSlot.SaveSlot

    Public ReadOnly Property DisplayName As String Implements ISaveSlot.DisplayName

    Public ReadOnly Property Filename As String Implements ISaveSlot.Filename
End Class
