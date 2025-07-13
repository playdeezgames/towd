Imports Terminal.Gui.Trees

Friend Class VerbCategoryTreeNode
    Inherits TreeNode

    Private descriptor As business.IVerbCategoryType

    Public Sub New(descriptor As business.IVerbCategoryType)
        MyBase.New(descriptor.Name)
        Me.descriptor = descriptor
    End Sub
End Class
