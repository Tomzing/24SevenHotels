import React, { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import './BookingForm.css';

const BookingForm = () => {
  const { register, handleSubmit, formState: { errors } } = useForm();
  const [rooms, setRooms] = useState([]);

  useEffect(() => {
    const fetchRooms = async () => {
      try {
        const response = await axios.get('http://localhost:5193/api/rooms');
        setRooms(response.data);
      } catch (error) {
        console.error('Error fetching rooms:', error);
      }
    };

    fetchRooms();
  }, []);


  // Whenever the room are changed within the dropdown menu, the user should get details on the selected room. 
  // Ideally should be a list of rooms, and then you can choose the room to book. 
  const handleRoomChange = async (event) => {
    const roomId = event.target.value;
    setValue('roomId', roomId);

    if (roomId) {
      try {
        const response = await axios.get(`http://localhost:5193/rooms/${roomId}`);
        console.log(response.data);
        setSelectedRoomDetails(response.data);
      } catch (error) {
        console.error('Error fetching room details: ', error);
        setSelectedRoomDetails(null);
      }
    } else {
      setSelectedRoomDetails(null);
    }
  };

  

  const onSubmit = async (data) => {
    try {
      const response = await axios.post('http://localhost:5193/api/bookings', data);
      alert('Booking created successfully!');
    } catch (error) {
      if (error.response && error.response.status === 409) {
        alert('The room is already booked during the requested timeslot.');
      } else {
        alert('An error occurred while creating the booking.');
      }
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <div className="bookingform-list-content-container">
        <label htmlFor="roomId">Rom</label>
        <select className="bookingform-input" id="roomId" onChange={handleRoomChange} {...register('roomId', { required: 'Room is required' }) }>
          <option value="">Velg et rom</option>
          {rooms.map((room) => (
            <option key={room.id} value={room.id}>
              {room.roomNumber}
            </option>
          ))}
        </select>
        {errors.roomId && <span>{errors.roomId.message}</span>}
      </div>

      <div className="bookingform-list-content-container">
        <label htmlFor="checkinDate">Check-in Dato</label>
        <input
          type="date"
          id="checkinDate"
          className="bookingform-input"
          {...register('checkinDate', { required: 'Check-in date is required' })}
        />
        {errors.checkinDate && <span>{errors.checkinDate.message}</span>}
      </div>

      <div className="bookingform-list-content-container">
        <label htmlFor="checkoutDate">Check-out Dato</label>
        <input
          type="date"
          id="checkoutDate"
          className="bookingform-input"
          {...register('checkoutDate', { required: 'Check-out date is required' })}
        />
        {errors.checkoutDate && <span>{errors.checkoutDate.message}</span>}
      </div>

      <button type="submit">Reserver rom</button>
    </form>
  );
};

export default BookingForm;