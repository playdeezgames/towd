Imports Terminal.Gui.Trees
Imports towd.business

Friend Class VerbMenuState
    Inherits ChildView

    Private Const AVAILABLE_TEXT As String = "Available"
    Private Const ALL_TEXT As String = "All"
    Private ReadOnly availableVerbTreeView As TreeView
    Private ReadOnly allVerbListView As ListView
    Private ReadOnly tabView As TabView
    Private lastVerb As IVerbType
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Verbs (Esc to cancel, F1 for help)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)

        allVerbListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        allVerbListView.SetSource(VerbTypes.Descriptors.Values.ToList)
        AddHandler allVerbListView.OpenSelectedItem, AddressOf OnAllVerbListViewOpenSelectedItem

        Dim allVerbListTab = New TabView.Tab With
            {
                .View = allVerbListView,
                .Text = ALL_TEXT
            }

        availableVerbTreeView = New TreeView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler availableVerbTreeView.ObjectActivated, AddressOf OnAvailableVerbTreeViewObjectActivated
        Dim availableVerbTreeTab = New TabView.Tab With
            {
                .View = availableVerbTreeView,
                .Text = AVAILABLE_TEXT
            }


        tabView = New TabView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 1
            }
        tabView.AddTab(availableVerbTreeTab, True)
        tabView.AddTab(allVerbListTab, False)
        Add(tabView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(tabView)
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub OnAvailableVerbTreeViewObjectActivated(args As ObjectActivatedEventArgs(Of ITreeNode))
        Dim verbTreeNode As VerbTreeNode = TryCast(args.ActivatedObject, VerbTreeNode)
        If verbTreeNode IsNot Nothing Then
            Dim descriptor = verbTreeNode.Descriptor
            If descriptor.CanPerform(World.Avatar) Then
                descriptor.Perform(World.Avatar)
                LastVerb = descriptor
                UpdateView()
            Else
                MessageBox.ErrorQuery("Sorry Not Sorry!", "You cannot do that.", "OK")
            End If
        End If
    End Sub

    Private Sub OnAllVerbListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim descriptor = CType(args.Value, IVerbType)
        If descriptor.CanPerform(World.Avatar) Then
            descriptor.Perform(World.Avatar)
            LastVerb = descriptor
            UpdateView()
        Else
            MessageBox.ErrorQuery("Sorry Not Sorry!", "You cannot do that.", "OK")
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        UpdateAvailableVerbTree()
        MyBase.UpdateView()
    End Sub

    Private Sub UpdateAvailableVerbTree()
        Dim character = World.Avatar
        Dim verbCategories = character.GetDoableVerbs().GroupBy(Function(x) x.VerbCategoryType)
        Dim selectedNode As ITreeNode = Nothing
        availableVerbTreeView.ClearObjects()
        For Each categoryEntry In verbCategories
            If categoryEntry.Count = 1 Then
                Dim verbNode As TreeNode = New VerbTreeNode(categoryEntry.Single)
                If categoryEntry.Single.VerbType = lastVerb?.VerbType Then
                    selectedNode = verbNode
                End If
                availableVerbTreeView.AddObject(verbNode)
            Else
                Dim categoryNode As TreeNode = New VerbCategoryTreeNode(categoryEntry.Key.ToDescriptor)
                availableVerbTreeView.AddObject(categoryNode)
                Dim children As New List(Of ITreeNode)
                Dim found As Boolean
                For Each verbEntry In categoryEntry
                    Dim verbNode As TreeNode = New VerbTreeNode(verbEntry.VerbType.ToDescriptor)
                    children.Add(verbNode)
                    If verbEntry.VerbType = lastVerb?.VerbType Then
                        found = True
                        selectedNode = verbNode
                    End If
                Next
                categoryNode.Children = children
                If found Then
                    availableVerbTreeView.ExpandAll(categoryNode)
                End If
            End If
            If selectedNode IsNot Nothing Then
                availableVerbTreeView.SelectedObject = selectedNode
            End If
        Next
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            CloseWindow()
        ElseIf args.KeyEvent.Key = Key.F1 Then
            args.Handled = True
            Select Case tabView.SelectedTab.Text
                Case ALL_TEXT
                    TopicState.Topic = VerbTypeTopicTable(CType(allVerbListView.Source.ToList(allVerbListView.SelectedItem), IVerbType).VerbType)
                Case AVAILABLE_TEXT
                    MessageBox.ErrorQuery("TODO!", "Need to implement F1 for this page!", "OK")
                    Return
            End Select
            ShowState(GameState.Topic)
        End If
    End Sub

    Private Sub CloseWindow()
        ShowState(GameState.Neutral)
    End Sub
End Class
