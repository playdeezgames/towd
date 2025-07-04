Imports Terminal.Gui.Trees
Imports towd.business

Friend Class VerbTreeNode
    Inherits TreeNode

    Public ReadOnly Property Descriptor As IVerbType

    Public Sub New(descriptor As IVerbType)
        MyBase.New(descriptor.Name)
        Me.descriptor = descriptor
    End Sub
End Class
