import {
  StatusBar,
  StyleSheet,
  View,
  Text,
  FlatList,
  Button,
  TextInput,
  Alert,
} from 'react-native';
import {
  SafeAreaProvider,
  useSafeAreaInsets,
} from 'react-native-safe-area-context';
import { useState, useEffect } from 'react';
import { Provider, useDispatch, useSelector } from 'react-redux';
import shipService from './composable/shipServices';
import authService from './composable/authService';
import Ship from './component/Ship';
import EditShip from './component/EditShip';
import NavigateShip from './component/NavigateShip';
import { login, logout } from './src/counterSlice';
import store from './src/store';
import Register from './component/Register';


function App() {
  return (
    <Provider store={store}>
      <SafeAreaProvider>
        <StatusBar barStyle="dark-content" />
        <AppContent />
      </SafeAreaProvider>
    </Provider>
  );
}

function AppContent() {
  const insets = useSafeAreaInsets();
  const dispatch = useDispatch();
  const isConnected = useSelector((state: any) => state.counter.isConnected);

  const { postShip, getShips, deleteShip } = shipService();
  const { loginUser, logoutUser} = authService();
  const [showRegister, setShowRegister] = useState(false);

  const [ships, setShips] = useState<Ship[]>([]);
  const [editingShip, setEditingShip] = useState<Ship | null>(null);
   const [navigatingShip, setNavigatingShip] = useState<Ship | null>(null);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const [name, setName] = useState('');
  const [captain, setCaptain] = useState('');
  const [crewSize, setCrewSize] = useState('');
  const [goldCargo, setGoldCargo] = useState('');

  const loadShips = async () => {
    try {
      setShips(await getShips());
    } catch (error) {
      Alert.alert('Erreur chargement', String(error));
    }
  };

  useEffect(() => {
  if (isConnected) {
    loadShips();
  } else {
    setShips([]); 
  }
}, [isConnected]);

  const handleCreateShip = async () => {
  if (name.length < 2 || name.length > 100) {
    Alert.alert('Nom invalide', '2 à 100 caractères.');
    return;
  }
  if (captain.length < 2 || captain.length > 50) {
    Alert.alert('Capitaine invalide', '2 à 50 caractères.');
    return;
  }
  const crew = Number(crewSize);
  const gold = Number(goldCargo);
  if (crew < 1 || crew > 500) {
    Alert.alert('Équipage invalide', '1 à 500.');
    return;
  }
  if (gold < 0 || gold > 1000000) {
    Alert.alert('Or invalide', '0 à 1 000 000.');
    return;
  }

  try {
    const ship = await postShip(
      { name, captain, crewSize: crew, goldCargo: gold },
      username
    );
    setShips([...ships, ship]);
    setName('');
    setCaptain('');
    setCrewSize('');
    setGoldCargo('');
  } catch (error) {
    Alert.alert('Erreur création', String(error));
  }
};

  const handleDelete = (ship: Ship) => {
    Alert.alert('Supprimer', `Supprimer ${ship.name} ?`, [
      { text: 'Annuler', style: 'cancel' },
      {
        text: 'Supprimer',
        style: 'destructive',
        onPress: async () => {
          try {
            await deleteShip(ship.id);
            await loadShips();
          } catch (error) {
            Alert.alert('Erreur suppression', String(error));
          }
        },
      },
    ]);
  };

  const handleLogin = async () => {
    try {
      await loginUser(username, password);
      dispatch(login());
      setPassword('');
    } catch (error) {
      Alert.alert('Erreur de connexion', String(error));
    }
  };

  const handleLogout = async () => {
    try {
      await logoutUser();
    } catch (error) {
      Alert.alert('Erreur logout :', String(error));
    }
    dispatch(logout());
  };

 if (showRegister) {
  return (
    <View style={{ flex: 1, paddingTop: insets.top, paddingBottom: insets.bottom }}>
      <Register
        onBack={() => setShowRegister(false)}
        onRegistered={(name) => {
          setUsername(name);
          setShowRegister(false);
        }}
      />
    </View>
  );
}

  // Page de modification
  if (editingShip) {
    return (
      <View style={{ flex: 1, paddingTop: insets.top, paddingBottom: insets.bottom }}>
        <EditShip
          ship={editingShip}
          onCancel={() => setEditingShip(null)}
          onSaved={async () => {
            setEditingShip(null);
            await loadShips();
          }}
        />
      </View>
    );
  }
if (navigatingShip) {
    return (
      <View style={{ flex: 1, paddingTop: insets.top, paddingBottom: insets.bottom }}>
        <NavigateShip
          ship={navigatingShip}
          onBack={() => setNavigatingShip(null)}
          onSailed={async () => {
            setNavigatingShip(null);
            await loadShips();
          }}
        />
      </View>
    );
  }
  return (
    <View
      style={[
        styles.container,
        { paddingTop: insets.top, paddingBottom: insets.bottom },
      ]}
    >
      {!isConnected ? (
        <View style={styles.section}>
          <Text style={styles.statusOff}>Vous n'êtes pas connecté</Text>

          <Text>Nom d'utilisateur</Text>
          <TextInput
            style={styles.input}
            value={username}
            onChangeText={setUsername}
            autoCapitalize="none"
          />

          <Text>Mot de passe</Text>
          <TextInput
            style={styles.input}
            value={password}
            onChangeText={setPassword}
            secureTextEntry={true}
          />

          <Button title="Se connecter" onPress={handleLogin} />
          <View style={{ height: 10 }} />
          <Button title="Créer un compte" onPress={() => setShowRegister(true)} />
        </View>
      ) : (
        <View style={styles.section}>
          <Text style={styles.statusOn}> Connecté en tant que {username}</Text>

          <TextInput
            style={[styles.input, { marginTop: 10 }]}
            placeholder="Nom du bateau (2-100)"
            placeholderTextColor="#888"
            value={name}
            onChangeText={setName}
          />
          <TextInput
            style={[styles.input, { marginTop: 6 }]}
            placeholder="Capitaine (2-50)"
            placeholderTextColor="#888"
            value={captain}
            onChangeText={setCaptain}
          />
          <TextInput
            style={[styles.input, { marginTop: 6 }]}
            placeholder="Équipage (1-500)"
            placeholderTextColor="#888"
            value={crewSize}
            onChangeText={setCrewSize}
            keyboardType="numeric"
          />
          <TextInput
            style={[styles.input, { marginTop: 6 }]}
            placeholder="Or (0-1 000 000)"
            placeholderTextColor="#888"
            value={goldCargo}
            onChangeText={setGoldCargo}
            keyboardType="numeric"
          />
          <View style={{ marginTop: 8 }}>
            <Button title="Ajouter un bateau" onPress={handleCreateShip} />
          </View>
        </View>
      )}

      <Text style={styles.listTitle}>Liste des bateaux</Text>
       <Text style={styles.listTitle}>
          Bateaux dans le port : {ships.length} / 8
      </Text>
      <FlatList
        style={styles.shipList}
        data={ships}
        keyExtractor={(item) => item.id}
        renderItem={({ item: ship }) => (
          <View style={styles.shipItem}>
            <Text style={styles.shipName}>{ship.name}</Text>
            <Text>Capitaine : {ship.captain}</Text>
            <Text>Équipage : {ship.crewSize}</Text>
            <Text>Or : {ship.goldCargo}</Text>
            <Text>Statut : {ship.status}</Text>
            {isConnected && (
              <View style={styles.actions}>
                <Button title="Modifier" onPress={() => setEditingShip(ship)} />
                  <Button title="Faire naviguer" onPress={() => setNavigatingShip(ship)} />
                <Button
                  title="Supprimer"
                  color="#d9534f"
                  onPress={() => handleDelete(ship)}
                />
              </View>
            )}
          </View>
        )}
      />

      {isConnected && (
        <View style={styles.logoutContainer}>
          <Button title="Se déconnecter" color="#d9534f" onPress={handleLogout} />
        </View>
      )}
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#fff', padding: 20 },
  section: { marginBottom: 20 },
  input: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 6,
    paddingHorizontal: 10,
    paddingVertical: 8,
    color: '#000',
    height: 40,
  },
  listTitle: { fontWeight: 'bold', marginBottom: 8 },
  shipList: { borderWidth: 1, borderColor: '#ccc', borderRadius: 6 },
  statusOn: { color: 'green', fontWeight: 'bold', fontSize: 16 },
  statusOff: { color: 'red', fontWeight: 'bold', fontSize: 16, marginBottom: 10 },
  logoutContainer: { marginTop: 10 },
  shipItem: {
    padding: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
  },
  shipName: { fontWeight: 'bold', fontSize: 16 },
  actions: { flexDirection: 'row', gap: 8, marginTop: 8 },
});

export default App;