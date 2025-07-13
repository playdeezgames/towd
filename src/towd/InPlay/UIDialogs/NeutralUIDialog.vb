Friend Module NeutralUIDialog
    Function DetermineInPlayDialog(context As IUIContext) As (String, IUIDialog)
        context.SaveGame(SaveSlot.Auto, Sub() Return)
        Dim character = context.World.Avatar
        If character.HasMessages Then
            Return (Nothing, New MessageUIDialog(context))
        ElseIf character.IsDead Then
            Return (Nothing, New DeadUIDialog(context))
        Else
            Return (Nothing, New NavigationUIDialog(context))
        End If
    End Function
End Module
