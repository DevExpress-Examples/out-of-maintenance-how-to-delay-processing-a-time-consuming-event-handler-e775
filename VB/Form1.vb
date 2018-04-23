Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace DelayedFocusChangeProcessing
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		Private panel1 As System.Windows.Forms.Panel
		Private WithEvents checkEdit1 As DevExpress.XtraEditors.CheckEdit
		Private WithEvents timer1 As System.Windows.Forms.Timer
		Private label1 As System.Windows.Forms.Label
		Private components As System.ComponentModel.IContainer

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.label1 = New System.Windows.Forms.Label()
			Me.checkEdit1 = New DevExpress.XtraEditors.CheckEdit()
			Me.timer1 = New System.Windows.Forms.Timer(Me.components)
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.panel1.SuspendLayout()
			CType(Me.checkEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.EmbeddedNavigator.Name = ""
			Me.gridControl1.Location = New System.Drawing.Point(0, 35)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(512, 330)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
'			Me.gridView1.FocusedRowChanged += New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(Me.gridView1_FocusedRowChanged);
			' 
			' panel1
			' 
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Controls.Add(Me.checkEdit1)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
			Me.panel1.Location = New System.Drawing.Point(0, 0)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(512, 35)
			Me.panel1.TabIndex = 1
			' 
			' label1
			' 
			Me.label1.Location = New System.Drawing.Point(253, 7)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(127, 20)
			Me.label1.TabIndex = 1
			Me.label1.Text = "label1"
			' 
			' checkEdit1
			' 
			Me.checkEdit1.EditValue = True
			Me.checkEdit1.Location = New System.Drawing.Point(20, 7)
			Me.checkEdit1.Name = "checkEdit1"
			Me.checkEdit1.Properties.Caption = "Delayed processing"
			Me.checkEdit1.Size = New System.Drawing.Size(173, 19)
			Me.checkEdit1.TabIndex = 0
'			Me.checkEdit1.CheckedChanged += New System.EventHandler(Me.checkEdit1_CheckedChanged);
			' 
			' timer1
			' 
			Me.timer1.Interval = 300
'			Me.timer1.Tick += New System.EventHandler(Me.timer1_Tick);
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(512, 365)
			Me.Controls.Add(Me.gridControl1)
			Me.Controls.Add(Me.panel1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.panel1.ResumeLayout(False)
			CType(Me.checkEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Public Function CreateTable() As DataTable
			Dim table As New DataTable()
			table.Columns.Add("ID", GetType(Integer))
			table.Columns.Add("RowName")

			Const RowCount As Integer = 1000
			For i As Integer = 0 To RowCount - 1
				table.Rows.Add(New Object() {i, "Row " & (i + 1).ToString()})
			Next i
			Return table
		End Function
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			gridControl1.DataSource = CreateTable()
		End Sub

		Public Sub ProcessFocusedRowChanged()
			' some time consuming process
			For i As Integer = 0 To 999999
				i = (i * 3) / 3
			Next i
			' The indicator that the process is completed
			label1.Text = gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, gridView1.Columns("RowName"))
			label1.Refresh()
		End Sub

		Private Sub gridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gridView1.FocusedRowChanged
			If checkEdit1.Checked Then
				If timer1.Enabled Then
					timer1.Enabled = False ' reset timer
				End If
				timer1.Enabled = True ' start timer
			Else
				ProcessFocusedRowChanged()
			End If
		End Sub
		Private Sub timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timer1.Tick
			timer1.Enabled = False ' self-stop timer
			ProcessFocusedRowChanged()
		End Sub

		Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkEdit1.CheckedChanged
			gridControl1.Focus()
		End Sub
	End Class
End Namespace
