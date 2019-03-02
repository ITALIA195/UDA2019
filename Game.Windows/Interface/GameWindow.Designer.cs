﻿using Game.Windows.Events;

namespace Game.Windows.Interface
{
    partial class GameWindow
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTime = new Game.Windows.Interface.AdjustingLabel();
            this.lblScore = new Game.Windows.Interface.AdjustingLabel();
            this.keyboard1 = new Game.Windows.Interface.Keyboard();
            this.guessField = new Game.Windows.Interface.GuessField();
            this.roundChange = new Game.Windows.Interface.RoundChange();
            this.audioPlayer = new Game.Windows.AudioPlayer(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(657, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Passa il turno";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(59, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Punteggio";
            // 
            // lblPlayer
            // 
            this.lblPlayer.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblPlayer.Location = new System.Drawing.Point(1, 9);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(799, 25);
            this.lblPlayer.TabIndex = 6;
            this.lblPlayer.Text = "Giocatore 1";
            this.lblPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(605, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tempo rimanente";
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 52.8804F);
            this.lblTime.Location = new System.Drawing.Point(436, 57);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(285, 81);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "100";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblScore
            // 
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 52.80907F);
            this.lblScore.Location = new System.Drawing.Point(63, 57);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(347, 81);
            this.lblScore.TabIndex = 4;
            this.lblScore.Text = "100";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // keyboard1
            // 
            this.keyboard1.DisabledKeyColor = System.Drawing.Color.DimGray;
            this.keyboard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.keyboard1.ForeColor = System.Drawing.Color.DarkGray;
            this.keyboard1.KeyColor = System.Drawing.Color.Gainsboro;
            this.keyboard1.Location = new System.Drawing.Point(97, 235);
            this.keyboard1.Name = "keyboard1";
            this.keyboard1.Size = new System.Drawing.Size(597, 182);
            this.keyboard1.TabIndex = 1;
            this.keyboard1.Text = "keyboard1";
            this.keyboard1.KeyPressed += new System.EventHandler<Game.Windows.Events.KeyboardEventArgs>(this.OnKeyPressed);
            // 
            // guessField
            // 
            this.guessField.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.guessField.Location = new System.Drawing.Point(12, 169);
            this.guessField.Name = "guessField";
            this.guessField.Size = new System.Drawing.Size(767, 60);
            this.guessField.TabIndex = 0;
            this.guessField.Text = "Charlotte";
            this.guessField.Word = "Charlotte";
            this.guessField.WordGuessed += new System.EventHandler(this.OnWordGuessed);
            // 
            // roundChange
            // 
            this.roundChange.Location = new System.Drawing.Point(1, 1);
            this.roundChange.Name = "roundChange";
            this.roundChange.Size = new System.Drawing.Size(799, 150);
            this.roundChange.TabIndex = 7;
            this.roundChange.Visible = false;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.keyboard1);
            this.Controls.Add(this.guessField);
            this.Controls.Add(this.roundChange);
            this.MaximizeBox = false;
            this.Name = "GameWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Song Guesser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GuessField guessField;
        private Keyboard keyboard1;
        private System.Windows.Forms.Button button1;
        private AdjustingLabel lblScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPlayer;
        private RoundChange roundChange;
        private AdjustingLabel lblTime;
        private System.Windows.Forms.Label label2;
        private AudioPlayer audioPlayer;
    }
}

