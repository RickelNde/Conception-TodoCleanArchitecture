import { useState } from 'react';
import { Alert, Button, StyleSheet, Text, TextInput, View } from 'react-native';
import { useDispatch } from 'react-redux';
import authService from '../composable/authService';
import { login } from '../src/counterSlice';

type Props = {
  onBack: () => void;
  onRegistered: (username: string) => void;
};

export default function Register({ onBack, onRegistered }: Props) {
  const dispatch = useDispatch();
  const { registerUser, loginUser } = authService();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleRegister = async () => {
    if (username === '' || password === '') {
      Alert.alert('Champs requis', 'Entre un nom et un mot de passe.');
      return;
    }
    if (password.length < 15) {
      Alert.alert('Mot de passe trop court', 'Le mot de passe doit contenir au moins 15 caractères.');
      return;
    }
    if (username.length < 3) {
      Alert.alert('Nom invalide', "Le nom d'utilisateur doit contenir au moins 3 caractères.");
      return;
    }
    try {
      await registerUser(username, password);
      await loginUser(username, password);
      dispatch(login());
      onRegistered(username);
    } catch (error) {
      Alert.alert("Erreur d'inscription");
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Créer un compte</Text>

      <Text>Nom d'utilisateur</Text>
      <TextInput
        style={styles.input}
        placeholder="Nom d'utilisateur"
        placeholderTextColor="#888"
        value={username}
        onChangeText={setUsername}
        autoCapitalize="none"
      />

      <Text>Mot de passe</Text>
      <TextInput
        style={styles.input}
        placeholder="Mot de passe"
        placeholderTextColor="#888"
        value={password}
        onChangeText={setPassword}
        secureTextEntry={true}
      />

      <Button title="Créer mon compte" onPress={handleRegister} />
      <View style={{ height: 10 }} />
      <Button title="Retour à la connexion" color="#888" onPress={onBack} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#fff', padding: 20 },
  title: { fontSize: 20, fontWeight: 'bold', marginBottom: 16 },
  input: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 6,
    paddingHorizontal: 10,
    height: 40,
    color: '#000',
    marginBottom: 12,
  },
});