Friend MustInherit Class ChildView
    Inherits View
    Protected ReadOnly mainView As MainView
    Sub New(mainView As MainView)
        Me.mainView = mainView
        Width = [Dim].Fill()
        Height = [Dim].Fill()
    End Sub
    Friend MustOverride Sub UpdateView()
End Class
