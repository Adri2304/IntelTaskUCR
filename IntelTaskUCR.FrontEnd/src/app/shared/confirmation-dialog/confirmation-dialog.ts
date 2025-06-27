import { Component, Inject } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IContentConfirmDialog } from '../../models/icontent-confirm-dialog';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-confirmation-dialog',
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './confirmation-dialog.html',
  styleUrl: './confirmation-dialog.css'
})
export class ConfirmationDialog {
  constructor(@Inject(MAT_DIALOG_DATA) public data: IContentConfirmDialog) { }
}
