import useFetch from './useFetch';

type Login = {
  username: string;
  password: string;
};

const authService = () => {
  const { POST } = useFetch();

  const loginUser = async (username: string, password: string) => {
    const data: Login = { username, password };
    return await POST<Login, any>('Auth/login', data);
  };
   const registerUser = async (username: string, password: string) => {
    const data: Login = { username, password };
    return await POST<Login, any>('Auth/register', data);
  };

  const logoutUser = async () => {
    return await POST<object, any>('Auth/logout', {});
  };

  return { loginUser,registerUser, logoutUser };
};

export default authService;