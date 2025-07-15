Imports towd.business

Friend Module NeutralUIDialog
    Function DetermineInPlayDialog(context As IUIContext(Of IWorld)) As IUIDialog
        context.SaveGame(SaveSlot.Auto)
        Dim character = context.World.Avatar
        If character.HasMessages Then
            Return New MessageUIDialog(context, Function() DetermineInPlayDialog(context))
        ElseIf character.IsDead Then
            Return New DeadUIDialog(context)
        Else
            Return New NavigationUIDialog(context)
        End If
    End Function
End Module
