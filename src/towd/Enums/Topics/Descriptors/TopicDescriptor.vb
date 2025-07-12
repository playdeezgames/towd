Public Class TopicDescriptor
    Implements ITopic
    Sub New(topic As String, title As String, content As String)
        Me.Topic = topic
        Me.Title = title
        Me.Content = content
    End Sub
    Public ReadOnly Property Topic As String Implements ITopic.Topic
    Public ReadOnly Property Title As String Implements ITopic.Title
    Public ReadOnly Property Content As String Implements ITopic.Content
End Class
