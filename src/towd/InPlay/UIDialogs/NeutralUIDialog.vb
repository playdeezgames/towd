Friend Module NeutralUIDialog
    Function DetermineInPlayDialog(context As IUIContext) As (String, IUIDialog)
        context.SaveGame(SaveSlot.Auto, Sub() Return)
        Dim character = context.World.Avatar
        If character.IsDead Then
            Return (GameState.Dead, Nothing)
        Else
            Return (GameState.Navigation, Nothing)
        End If
    End Function
End Module
