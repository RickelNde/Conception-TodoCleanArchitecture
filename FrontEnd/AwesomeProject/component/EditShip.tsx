import { useState } from 'react';
import { Alert, Button, StyleSheet, Text, TextInput, View } from 'react-native';
import Ship from './Ship';
import shipService from '../composable/shipServices';

type Props = {
  ship: Ship;
  onSaved: () => void;
  onCancel: () => void;
};

export default function EditShip({ ship, onSaved, onCancel }: Props) {
  const { putShip } = shipService();
  const [name, setName] = useState(ship.name);
  const [captain, setCaptain] = useState(ship.captain);
  const [crewSize, setCrewSize] = useState(String(ship.crewSize));
  const [goldCargo, setGoldCargo] = useState(String(ship.goldCargo));

  const handleSave = async () => {
    const crew = Number(crewSize);
    const gold = Number(goldCargo);

    if (name.length < 2 || name.length > 100) {
      Alert.alert('Nom invalide', '2 à 100 caractères.');
      return;
    }
    if (captain.length < 2 || captain.length > 50) {
      Alert.alert('Capitaine invalide', '2 à 50 caractères.');
      return;
    }
    if (crew < 1 || crew > 500) {
      Alert.alert('Équipage invalide', '1 à 500.');
      return;
    }
    if (gold < 0 || gold > 1000000) {
      Alert.alert('Or invalide', '0 à 1 000 000.');
      return;
    }

    try {
      await putShip({ ...ship, name, captain, crewSize: crew, goldCargo: gold });
      onSaved();
    } catch (error) {
      Alert.alert('Erreur modification', String(error));
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Modifier le bateau</Text>

      <Text>Nom</Text>
      <TextInput style={styles.input} value={name} onChangeText={setName} />

      <Text>Capitaine</Text>
      <TextInput style={styles.input} value={captain} onChangeText={setCaptain} />

      <Text>Équipage (1-500)</Text>
      <TextInput
        style={styles.input}
        value={crewSize}
        onChangeText={setCrewSize}
        keyboardType="numeric"
      />

      <Text>Or (0-1 000 000)</Text>
      <TextInput
        style={styles.input}
        value={goldCargo}
        onChangeText={setGoldCargo}
        keyboardType="numeric"
      />

      <View style={{ marginTop: 20 }}>
        <Button title="Enregistrer" onPress={handleSave} />
        <View style={{ height: 10 }} />
        <Button title="Annuler" color="#888" onPress={onCancel} />
      </View>
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
    marginBottom: 10,
  },
});