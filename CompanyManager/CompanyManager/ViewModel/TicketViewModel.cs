using BLL.Services;
using CompanyManager.Core;
using CompanyManager.View;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CompanyManager.ViewModel
{
    public class TicketViewModel : BaseViewModel
    {
        private readonly SolidColorBrush _defaultBgColor;
        private readonly SolidColorBrush _premiumBgColor;
        private readonly SolidColorBrush _defaultBorderColor;
        private readonly SolidColorBrush _premiumBorderColor;


        private readonly TicketService _ticketService;
        private readonly SessionHallService _sessionHallService;
        private Dictionary<string, Grid> seatsGrids;


        public TicketViewModel(TicketService Tservice, SessionHallService Sservice)
        {
            this._ticketService = Tservice;
            this._sessionHallService = Sservice;

            seatsGrids = new();
            _defaultBorderColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#abaaaa");
            _premiumBorderColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#ff0047");
            _defaultBgColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#959595");
            _premiumBgColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#ff0067");
            LoadSession();

        }
        private Employee currentWorker;
        public Employee CurrentWorker
        {

            set { if (currentWorker == null) currentWorker = value; }
        }
        private Session currentSession;
        public Session CurrentSession
        {
            get { if (currentSession == null && Sessions != null) currentSession = new(); return currentSession; }
            set { currentSession = value; base.OnPropertyChanged("CurrentSession"); }
        }

        private ObservableCollection<Session> sessions;
        public ObservableCollection<Session> Sessions
        {
            get { if (sessions == null) sessions = new(); return sessions; }
        }


        private async void LoadSession()
        {
            try
            {
                var sess = await _sessionHallService.GetAllNotFinishedSessionsAsync();
                foreach (var item in sess)
                {
                    Sessions.Add(item);

                }
            }
            catch
            {
                Thread.Sleep(600);
                LoadSession();
            }

        }


        private Grid currentGrid;
        public UIElement CurrentGrid
        {
            get { if (currentGrid == null) currentGrid = new Grid(); return currentGrid; }
            set { currentGrid = (Grid)value; base.OnPropertyChanged("CurrentGrid"); }
        }

        private ICommand setCommand;
        public ICommand SetCommand
        {
            get { if (setCommand == null) setCommand = new RelayCommand(SetGridExecute, SetGridCanExecute); return setCommand; }
        }

        private async void SetGridExecute(object obj)
        {
            Grid grid = null;
            try
            {
                grid = seatsGrids[currentSession.ToString()];
            }
            catch { }
            if (grid != null)
            { CurrentGrid = grid; return; }
            grid = new();
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < currentSession.Hall.Rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < currentSession.Hall.SeatsInRow; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < currentSession.Hall.Rows; i++)
                for (int j = 0; j < currentSession.Hall.SeatsInRow; j++)
                {
                    var bookings = currentSession.Bookings.Where(x => x.Seat.Row == i + 1 && x.Seat.SeatInRow == j + 1);
                    var btn = new Button();
                    btn.BorderThickness = new System.Windows.Thickness(2);
                    btn.Command = BookingCommand;
                    btn.CommandParameter = btn;
                    btn.Height = 19;
                    btn.Width = 33;
                    btn.Margin = new Thickness(4);
                    btn.SetValue(Grid.RowProperty, i);
                    btn.SetValue(Grid.ColumnProperty, j);
                    if (bookings.Count() != 0)
                    {
                        var booking = bookings.First();
                        var seat = booking.Seat;

                        if (seat.Type == "Premium")
                        {
                            btn.BorderBrush = _premiumBorderColor;
                            if (booking.IsBooking == true)
                            {
                                if (booking.IsPaid == true)
                                    btn.Background = _premiumBorderColor;
                                else
                                    btn.Background = _premiumBgColor;
                            }
                            else btn.Background = null;
                        }
                        else if (seat.Type == "default")
                        {
                            btn.BorderBrush = _defaultBorderColor;
                            if (booking.IsBooking == true)
                            {
                                if (booking.IsPaid == true)

                                    btn.Background = _defaultBorderColor;
                                else
                                    btn.Background = _defaultBgColor;
                            }
                            else btn.Background = null;
                        }

                        btn.Tag = booking;
                        grid.Children.Add(btn);
                    }
                    else
                    {
                        var booking = new Booking() { Session = currentSession, Employee = currentWorker, IsBooking = false, IsPaid = false };
                        if(currentSession.Hall.Seats==null)
                        {
                            var sess = await _sessionHallService.GetSessionWithSeats(CurrentSession);
                            if (sess == null) return;
                            currentSession.Hall.Seats = sess.Hall.Seats;
                        }
                        var seat = currentSession.Hall.Seats.Where(x => x.Row == 1 + i && x.SeatInRow == 1 + j).First();
                        booking.Seat = seat;

                        btn.Background = null;

                        //"Premium" : "default"
                        if (seat.Type == "Premium")
                            btn.BorderBrush = _premiumBorderColor;
                        else if (seat.Type == "default")
                            btn.BorderBrush = _defaultBorderColor;
                        btn.Tag = booking;
                        grid.Children.Add(btn);
                        await _ticketService.AddBookingAsync(booking);
                    }
                }
            seatsGrids.Add(currentSession.ToString(), grid);
            CurrentGrid = grid;
        }
        private bool SetGridCanExecute(object obj)
        {
            return currentSession != null;
        }

        private ICommand bookingCommand;
        public ICommand BookingCommand
        {
            get { if (bookingCommand == null) bookingCommand = new RelayCommand(BookingExecute); return bookingCommand; }
        }
        private async void BookingExecute(object obj)
        {
            var btn = (Button)obj;
            var booking = (Booking)btn.Tag;
            var wind = new СhooseVariant($"Row:{booking.Seat.Row} Seat{booking.Seat.SeatInRow} Hall numm{booking.Session.Hall.HallNumber}", booking.IsBooking, booking.IsPaid);
            wind.ShowDialog();
            if (wind.DialogResult == false)
                return;
            if (wind.state == 1)
            {
                booking.IsBooking = true;
                booking.IsPaid = false;
                booking.ClientPhoneNumber = "  ";
                var res = await _ticketService.BookingTicketAsync(booking);
                if (res == false) return;
                btn.Background = booking.Seat.Type == "Premium" ? _premiumBgColor : _defaultBgColor;
            }
            else if (wind.state == 2)
            {
                booking.IsPaid = true;
                booking.ClientPhoneNumber = "  ";
                var res = await _ticketService.PaidTicketAsync(booking);
                if (res == false) return;
                btn.Background = booking.Seat.Type == "Premium" ? _premiumBorderColor : _defaultBorderColor;
            }
            else if (wind.state == 3)
            {
                var res = await _ticketService.CanselTicket(booking);
                if (res == false) return;
                btn.Background = null;
            }
        }
    }
}
