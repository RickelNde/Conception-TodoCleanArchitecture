import { createSlice } from '@reduxjs/toolkit';

export const counterSlice = createSlice({
  name: 'counter',

  initialState: {
    isConnected: false,
  },

  reducers: {
    login: (state) => {
      state.isConnected = true;
    },

    logout: (state) => {
      state.isConnected = false;
    },
  },
});

export const { login, logout } = counterSlice.actions;

export default counterSlice.reducer;