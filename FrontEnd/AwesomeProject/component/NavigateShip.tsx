
import { useEffect, useState } from 'react';
import {
  View,
  Text,
  Button,
  TouchableOpacity,
  ScrollView,
  ActivityIndicator,
  StyleSheet,
  Alert,
} from 'react-native';
import Ship from './Ship';
import shipService from '../composable/shipServices';

type Props = {
  ship: Ship;
  onBack: () => void;
  onSailed: () => void | Promise<void>;
};

export default function NavigateShip({ ship, onBack, onSailed }: Props) {
  const { getPorts, sailShip } = shipService();

  const [ports, setPorts] = useState<string[]>([]);
  const [selected, setSelected] = useState<string | null>(null);
  const [open, setOpen] = useState(false);
  const [loading, setLoading] = useState(true);
  const [sailing, setSailing] = useState(false);

  useEffect(() => {
    getPorts()
      .then(setPorts)
      .catch(() => Alert.alert('Erreur', 'Impossible de charger les ports.'))
      .finally(() => setLoading(false));
  }, []);

  const handleSend = async () => {
    if (!selected) return;

    setSailing(true);

    try {
      await sailShip(ship.id, selected);
      Alert.alert('En route', `${ship.name} navigue vers ${selected}.`);
      await onSailed();
    } catch (error) {
      Alert.alert('Erreur', 'Impossible de déplacer le bateau.');
    } finally {
      setSailing(false);
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Envoyer {ship.name}</Text>

      {loading ? (
        <ActivityIndicator size="large" />
      ) : (
        <>
          <Text style={styles.label}>Port de destination</Text>

          <TouchableOpacity
            style={styles.dropdown}
            onPress={() => setOpen(!open)}
          >
            <Text>{selected ?? 'Choisir un port'}</Text>
          </TouchableOpacity>

          {open && (
            <ScrollView style={styles.options}>
              {ports.map((port) => (
                <TouchableOpacity
                  key={port}
                  style={styles.option}
                  onPress={() => {
                    setSelected(port);
                    setOpen(false);
                  }}
                >
                  <Text>{port}</Text>
                </TouchableOpacity>
              ))}
            </ScrollView>
          )}

          <Button
            title={sailing ? 'Envoi en cours...' : 'Envoyer'}
            onPress={handleSend}
            disabled={!selected || sailing}
          />
        </>
      )}

      <View style={styles.back}>
        <Button title="Retour" color="#888" onPress={onBack} disabled={sailing} />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    padding: 20,
  },
  title: {
    fontWeight: 'bold',
    fontSize: 20,
    marginBottom: 16,
  },
  label: {
    marginBottom: 6,
  },
  dropdown: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 6,
    padding: 12,
    marginBottom: 10,
  },
  options: {
    maxHeight: 220,
    borderWidth: 1,
    borderColor: '#ccc',
    marginBottom: 10,
  },
  option: {
    padding: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#eee',
  },
  back: {
    marginTop: 10,
  },
});
